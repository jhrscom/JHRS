using JHRS.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JHRS.GenerateAPI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = "接口生成工具【首发于：jhrs.com】";
        }

        private string currentProject = "JHRS.GenerateAPI";//當前項目
        private string interfaceDir = "JHRS.Core\\Apis"; //生成的接口保存目錄
        private string modelDir = "JHRS.Core\\Models";   //生成的DTO(實體類)保存目錄
        private string enumDir = "JHRS.Core\\Enums";//生成的枚舉文件保存目錄

        /// <summary>
        /// 生成代码。现成工具生成代码比较冗余，就手动实现一个。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            //第一步，先提取接口名称和注释。
            //第二步：再提取每个接口下的方法名称，参数，注释，返回值等等
            //第三步：提取实体对象根据模板文件生成接口类，并写入对应文件。

            //第四步：先收集所有components节点（表示接口返回的对象）保存到一个集合里面
            //第五步：再用第4步操作的数据源去重，生成（DTO）实体类，用于WPF客户端调用传参，接口也引用；暂不分到各个目录，因为命名空间不好整。

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入接口地址！", "输入提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var names = Enum.GetNames(typeof(SystemModule));
            var path = AppDomain.CurrentDomain.BaseDirectory;

            List<CollectInterfaceClass> list = new List<CollectInterfaceClass>();
            List<CollectEntityClass> entityList = new List<CollectEntityClass>();
            List<CollectEnumClass> enumList = new List<CollectEnumClass>();

            foreach (var item in names)
            {
                var url = $"{textBox1.Text}/swagger/{item}/swagger.json";
                var data = await GetReponse(url);
                var arrary = data.SelectToken("paths");
                var tags = data.SelectToken("tags")?.ToArray();
                if (tags == null) continue;
                var myList = FindInterface(item, tags);

                var entitys = FindEntitys(data, item);
                entityList.AddRange(entitys);

                foreach (var c in myList)
                {
                    var methods = arrary.Where(x => c.Name == x.ElementAt(0).ElementAt(0).ElementAt(0).Value<JArray>("tags").First.ToString()).ToArray();
                    FindMethods(c, data, item, methods);
                }
                list.AddRange(myList);

                enumList.AddRange(FindEnums(data, item));
            }
            entityList = entityList.DistinctBy(x => x.Name).ToList();
            enumList = enumList.DistinctBy(x => x.Name).ToList();

            var interfacePath = path.Substring(0, path.IndexOf(currentProject)) + interfaceDir;
            Directory.CreateDirectory(interfacePath);
            var template = File.ReadAllText("template/ITemplate.txt");

            //生成接口
            foreach (var item in list)
            {
                var currentPath = interfacePath + $"\\{item.Module}";
                Directory.CreateDirectory(currentPath);
                var sourceTemp = template;
                var interfaceName = $"I{item.Name}Api";

                //返回值是类的引用
                List<string> usings = entityList.Where(x => item.Methods.Select(p => p.Return)
                   .Any(m => m.Contains(x.Name.Substring(x.Name.LastIndexOf('.') + 1))))
                   .Select(x => "using JHRS.Core.Models." + x.Module + ";").ToList();
                //返回值是枚举的引用
                var usingEnum = enumList.Where(x => item.Methods.Select(p => p.Return)
                    .Any(m => x.Name.Substring(x.Name.LastIndexOf('.') + 1).Contains(m)))
                    .Select(x => "using JHRS.Core.Enums." + x.Module + ";").ToList();
                usings.AddRange(usingEnum);
                //参数是类的引用 
                var usingPars = entityList.Where(x => item.Methods.SelectMany(p => p.ParameterType)
                     .Any(m => m.Contains(x.Name.Substring(x.Name.LastIndexOf('.') + 1))))
                    .Select(x => "using JHRS.Core.Models." + x.Module + ";").ToList();
                usings.AddRange(usingPars);
                //参数是枚举的引用
                usingPars = enumList.Where(x => item.Methods.SelectMany(p => p.ParameterType)
                     .Any(m => x.Name.Substring(x.Name.LastIndexOf('.') + 1).Contains(m)))
                    .Select(x => "using JHRS.Core.Enums." + x.Module + ";").ToList();
                usings.AddRange(usingPars);

                sourceTemp = sourceTemp.Replace("$当前时间$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("$模块名称$", item.Module)
                    .Replace("$接口注释$", item.Description)
                    .Replace("$接口名称$", interfaceName)
                    .Replace("$引用空间$", string.Join("\r\n", usings.Distinct()))
                    .Replace("$接口方法$", GenerationMethod(item));

                if (!File.Exists($"{currentPath}\\{interfaceName}.cs"))
                    File.WriteAllText($"{currentPath}\\{interfaceName}.cs", sourceTemp);

                richTextBox1.Text += sourceTemp + $"\r\n\r\n-------------------------------{ currentPath}\\{ interfaceName}.cs-----------------------------------------\r\n\r\n";
            }

            var modelsPath = path.Substring(0, path.IndexOf(currentProject)) + modelDir;
            Directory.CreateDirectory(modelsPath);

            var entityTemplate = File.ReadAllText("template/ClassTemplate.txt");

            //生成实体
            foreach (var item in entityList)
            {
                var currentPath = modelsPath + $"\\{item.Module}";
                Directory.CreateDirectory(currentPath);

                //属性是类的引用
                List<string> usings = entityList.Where(x =>
                    item.Properties.Select(p => p.PropertyType)
                    .Any(m => m.Contains(x.Name.Substring(x.Name.LastIndexOf('.') + 1))))
                    .Select(x => "using JHRS.Core.Models." + x.Module + ";").ToList();
                //属性是枚举的引用
                var usingEnum = enumList.Where(x =>
                    item.Properties.Select(p => p.PropertyType)
                    .Any(m => x.Name.Substring(x.Name.LastIndexOf('.') + 1).Contains(m)))
                    .Select(x => "using JHRS.Core.Enums." + x.Module + ";").ToList();
                usings.AddRange(usingEnum);

                var name = item.Name.Substring(item.Name.LastIndexOf('.') + 1);
                var sourceTemp = entityTemplate;
                sourceTemp = sourceTemp.Replace("$类名称$", name)
                    .Replace("$当前时间$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("$类注释$", item.Description)
                    .Replace("$模块名称$", item.Module)
                    .Replace("$引用空间$", string.Join("\r\n", usings.Distinct()))
                    .Replace("$属性$", GenerationProperty(item));


                if (!File.Exists($"{currentPath}\\{name}.cs"))
                    File.WriteAllText($"{currentPath}\\{name}.cs", sourceTemp);
            }

            var enumsPath = path.Substring(0, path.IndexOf(currentProject)) + enumDir;
            Directory.CreateDirectory(enumsPath);

            var enumTemplate = File.ReadAllText("template/EnumTemplate.txt");

            //生成枚举
            foreach (var item in enumList)
            {
                var currentPath = enumsPath + $"\\{item.Module}";
                Directory.CreateDirectory(currentPath);

                var name = item.Name.Substring(item.Name.LastIndexOf('.') + 1);
                var sourceTemp = enumTemplate;
                sourceTemp = sourceTemp.Replace("$类名称$", name)
                    .Replace("$当前时间$", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    .Replace("$类注释$", item.Description)
                    .Replace("$模块名称$", item.Module)
                    .Replace("$属性$", GenerationEnumProperty(item));

                if (!File.Exists($"{currentPath}\\{name}.cs"))
                    File.WriteAllText($"{currentPath}\\{name}.cs", sourceTemp);
            }

            MessageBox.Show("已生成完毕！", "生成提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        /// <summary>
        /// 生成枚举属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerationEnumProperty(CollectEnumClass model)
        {
            StringBuilder sb = new StringBuilder(200);
            foreach (var item in model.Properties)
            {
                sb.Append("        /// <summary>\r\n");
                sb.AppendFormat("        /// {0}\r\n", item.EnumName);
                sb.Append("        /// </summary>\r\n");
                sb.AppendFormat("        {0} = {1},\r\n\r\n", item.EnumName, item.EnumValue);
            }

            return sb.ToString().TrimEnd(",\r\n\r\n".ToArray());
        }

        /// <summary>
        /// 解析枚举集合
        /// </summary>
        /// <param name="data">json</param>
        /// <param name="module">所属模块</param>
        /// <returns></returns>
        private List<CollectEnumClass> FindEnums(JObject data, string module)
        {
            List<CollectEnumClass> list = new List<CollectEnumClass>();
            string[] excluded = new string[] { "OperationResultType", "ListSortDirection" };
            var arrary = data.SelectToken("components.schemas").Where(x => x.ElementAt(0).SelectToken("enum") != null && !excluded.Any(p => (x as JProperty).Name.Contains(p))).ToArray();
            foreach (var item in arrary)
            {
                var p = item.ElementAt(0).SelectToken("enum").ToList();
                List<EnumProperty> properties = new List<EnumProperty>();
                if (item.ElementAt(0).SelectToken("description") != null)
                {
                    var des = item.ElementAt(0).SelectToken("description").ToString().Split("<br/>\r\n");
                    for (int i = 0; i < p.Count; i++)
                    {
                        var name = p[i].ToString();
                        properties.Add(new EnumProperty
                        {
                            EnumName = des[i + 1].Replace("/", "").Split(':')[1].TrimEnd("; ".ToArray()),
                            EnumValue = name
                        });
                    }
                    list.Add(new CollectEnumClass
                    {
                        Module = module,
                        Name = (item as JProperty).Name,
                        Properties = properties,
                        Description = des[0]
                    });
                }

            }
            return list;
        }

        /// <summary>
        /// 生成属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GenerationProperty(CollectEntityClass model)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in model.Properties)
            {
                sb.Append("        /// <summary>\r\n");
                sb.AppendFormat("        /// {0}\r\n", item.Description);
                sb.AppendFormat("        /// </summary>\r\n");
                sb.AppendFormat("        public {0} {1} {{ get; set; }}\r\n\r\n", item.PropertyType, item.Name);
            }
            return sb.ToString().TrimEnd("\r\n\r\n".ToArray());
        }

        private string[] Excluded = new string[] { "FilterOperate", "PageRequest", "SortCondition", "ListSortDirection", "PageCondition", "FilterGroup", "FilterRule", "OperationResultType", "OperationResult", "PageData" };

        /// <summary>
        /// 查找实体类
        /// </summary>
        /// <param name="data">json</param>
        /// <param name="module">所属模块</param>
        /// <returns></returns>
        private List<CollectEntityClass> FindEntitys(JObject data, string module)
        {
            List<CollectEntityClass> list = new List<CollectEntityClass>();
            var arrary = data.SelectToken("components.schemas").ToArray();
            foreach (var item in arrary)
            {
                string className = (item as JProperty).Name;

                if (Excluded.Any(x => className.Contains(x))) continue;

                List<EntityProperty> properties = new List<EntityProperty>();

                var pro = item.ElementAt(0).SelectToken("properties");

                if (pro != null)
                {
                    pro.ToList().ForEach(p =>
                    {
                        var name = (p as JProperty).Name;
                        //属性分4种情况，第一：基本类型 第二：引用类型 第三：数组 第四：List 里面再嵌List或者单对象
                        var type = "";
                        if (p.ElementAt(0).SelectToken("type") != null)
                        {
                            type = p.ElementAt(0).SelectToken("type")?.ToString();
                        }
                        else if (p.ElementAt(0).SelectToken("$ref") != null)
                        {
                            type = p.ElementAt(0).SelectToken("$ref").ToString();
                        }
                        if (string.IsNullOrEmpty(type)) throw new Exception("获取属性类型失败！");
                        type = GetPropertyType(type, p.ElementAt(0));

                        if (type == "List")
                        {
                            var subType = "";
                            if (p.ElementAt(0).SelectToken("items.type") != null)
                            {
                                subType = p.ElementAt(0).SelectToken("items.type").ToString();
                            }
                            else if (p.ElementAt(0).SelectToken("items.$ref") != null)
                            {
                                subType = p.ElementAt(0).SelectToken("items.$ref").ToString();
                            }
                            if (string.IsNullOrEmpty(subType)) throw new Exception("获取子类型失败！");
                            type = $"List<{GetPropertyType(subType, p.ElementAt(0))}>";
                        }

                        if (string.IsNullOrWhiteSpace(type)) throw new Exception("属性类型不能为空！");

                        properties.Add(new EntityProperty
                        {
                            Description = (p.ElementAt(0).Value<string>("description") + "").Replace(Environment.NewLine, ""),
                            Format = p.ElementAt(0).Value<string>("format"),
                            IsNullable = p.ElementAt(0).Value<string>("nullable"),
                            Name = name,
                            PropertyType = type
                        });
                    });
                }


                if (properties.Count == 0) continue;
                //泛型类忽略掉
                if (className.Contains("`1")) continue;

                var nameSpace = list.FirstOrDefault(x => x.Module == module && x.Name == className);
                list.Add(new CollectEntityClass
                {
                    Module = nameSpace != null ? nameSpace.Module : module,
                    Name = className,
                    Description = item.SelectToken("description") + "",
                    Properties = properties
                });
            }
            return list;
        }

        private string GetPropertyType(string t, JToken node)
        {
            if (t == "integer") return "int";
            if (t == "boolean") return "bool";
            if (t == "number") return "decimal";
            if (t == "array") return "List";
            if (t == "string" && node.SelectToken("format")?.ToString() == "date-time") return "DateTime";
            if (t == "string") return "string";

            if (t.StartsWith("#"))
            {
                return t.Substring(t.LastIndexOf('.') + 1);
            }
            return t;
        }

        /// <summary>
        /// 生成方法
        /// </summary>
        /// <param name="model">方法类</param>
        /// <returns></returns>
        private string GenerationMethod(CollectInterfaceClass model)
        {
            StringBuilder sb = new StringBuilder(2000);

            foreach (var item in model.Methods)
            {
                string pars = "";

                sb.Append("        /// <summary>\r\n");
                sb.AppendFormat("        /// {0}\r\n", item.Description);
                sb.Append("        /// </summary>\r\n");

                if (item.ParameterName.Length > 0)
                {
                    for (int i = 0; i < item.ParameterName.Length; i++)
                    {
                        sb.AppendFormat("        /// <param name=\"{0}\">{1}</param>\r\n", item.ParameterName[i], item.ParameterDes[i]);

                        pars += $"{item.ParameterType[i]} {item.ParameterName[i]}, ";
                    }
                    pars = pars.TrimEnd(", ".ToArray());
                }
                sb.AppendFormat("        /// <returns>见返回参数</returns>\r\n");
                sb.AppendFormat("        [{0}(\"{1}\")]\r\n", item.MethodType.Replace("p", "P").Replace("g", "G"), item.Api);
                sb.AppendFormat("        Task<{0}> {1}({2});\r\n\r\n", item.Return, item.Name, pars);
            }

            return sb.ToString().TrimEnd("\r\n\r\n".ToArray());
        }

        /// <summary>
        /// 解析每个接口对应方法
        /// </summary>
        /// <param name="belong">当前隶属类</param>
        /// <param name="data">原始json</param>
        /// <param name="module">当前模块</param>
        /// <param name="methods">方法</param>
        private void FindMethods(CollectInterfaceClass belong, JObject data, string module, JToken[] methods)
        {
            var m = new List<Method>();
            foreach (var item in methods)
            {
                var api = (item as JProperty).Name;
                var (parsName, parsType, parsDes) = GetParameter(item);

                var first = (item.ElementAt(0).ElementAt(0) as JProperty).Name;
                m.Add(new Method
                {
                    Api = api,
                    Description = (item.ElementAt(0).SelectToken($"{first}.summary") + "").Replace(Environment.NewLine, ""),
                    MethodType = first,
                    Name = api.Substring(api.LastIndexOf('/') + 1),
                    ParameterName = parsName,
                    ParameterType = parsType,
                    ParameterDes = parsDes,
                    Return = FindReturn(item)
                });
            }
            belong.Methods = m;
        }

        /// <summary>
        /// 解析返回值
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private string FindReturn(JToken token)
        {
            if (token.ElementAt(0).SelectToken("post.responses.200.content") == null)
            {
                return "dynamic";
            }
            var re = token.ElementAt(0).SelectToken("post.responses.200.content.application/json.schema.$ref")?.ToString();
            if (re == null)
            {
                var t = token.ElementAt(0).SelectToken("post.responses.200.content.application/json.schema.type").ToString();
                re = token.ElementAt(0).SelectToken("post.responses.200.content.application/json.schema.items.$ref").ToString();
                re = re.Substring(re.LastIndexOf('.') + 1); //取得返回值
                if (t == "array") return $"List<{re}>";
            }
            if (re.EndsWith("OperationResult")) return "OperationResult";
            if (re.Contains("OperationResult`1"))
            {
                re = re.Substring(re.IndexOf("[[") + 2).TrimEnd("]]".ToArray());
                if (re.IndexOf("[[") > 0)
                {
                    var s = re.Substring(0, re.IndexOf("`1"));
                    s = s.Substring(s.LastIndexOf('.') + 1);

                    re = re.Substring(re.IndexOf("[[") + 2).TrimEnd("]]".ToArray());
                    re = re.Substring(0, re.IndexOf(","));
                    re = re.Substring(re.LastIndexOf('.') + 1); //取得返回值

                    return $"OperationResult<{s}<{re}>>";
                }
                else
                {
                    re = re.Substring(0, re.IndexOf(","));
                    re = re.Substring(re.LastIndexOf('.') + 1); //取得返回值

                    if (re == "Object") re = "object";
                    if (re == "Int32") re = "int";
                    if (re == "String") re = "string";

                    return $"OperationResult<{re}>";
                }
            }
            throw new Exception("未获取到返回值类型");
        }

        /// <summary>
        /// 解析参数类型，参数名，参数描述
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private (string[], string[], string[]) GetParameter(JToken token)
        {
            var first = (token.ElementAt(0).ElementAt(0) as JProperty).Name;

            var parsArrary = token.ElementAt(0).SelectToken($"{first}.parameters");
            var parsModel = token.ElementAt(0).SelectToken($"{first}.requestBody");
            List<string> parsName = new List<string>();
            List<string> parsType = new List<string>();
            List<string> parsDes = new List<string>();

            //无参函数
            if (parsArrary == null && parsModel == null) return (parsName.ToArray(), parsType.ToArray(), parsDes.ToArray());

            if (parsArrary != null)
            {
                foreach (var item in parsArrary)
                {
                    var s = item.Value<JObject>("schema").ToString();
                    var parType = item.Value<JObject>("schema").Value<string>("type");
                    if (parType == null) parType = item.Value<JObject>("schema").Value<string>("$ref");

                    parsType.Add(GetParsType(parType));
                    parsDes.Add(item.Value<string>("description") + "");
                    parsName.Add(item.Value<string>("name"));
                }
            }
            else
            {
                var content = parsModel.SelectToken("content.application/json");
                var parType = content.SelectToken("schema.$ref")?.ToString();
                if (null == parType && content.SelectToken("schema.type")?.ToString() == "array")
                {
                    parType = content.SelectToken("schema.items.$ref")?.ToString();
                    if (parType == null)
                    {
                        parType = content.SelectToken("schema.items.type").ToString();
                        parsType.Add(GetParsType(parType));
                    }
                    else if (parType != null && parType.StartsWith("#"))
                    {
                        var s = parType.Substring(parType.LastIndexOf('/') + 1);
                        parsType.Add($"List<{s.Substring(s.LastIndexOf('.') + 1)}>");
                    }
                    else
                    {
                        throw new Exception("参数类型匹配失败！");
                    }
                }
                else
                {
                    parsType.Add(GetParsType(parType));
                }
                parsName.Add("dto");

                var des = content.Value<string>("description");
                if (des == null) des = content.SelectToken("schema.description")?.ToString();

                parsDes.Add(des + "");
            }
            return (parsName.ToArray(), parsType.ToArray(), parsDes.ToArray());
        }

        private string GetParsType(string t)
        {
            if (t == "integer") return "int";
            if (t == "string") return "string";
            if (t == "boolean") return "bool";
            if (t == "number") return "decimal";

            if (t.StartsWith("#"))
            {
                var s = t.Substring(t.LastIndexOf('/') + 1);
                return s.Substring(s.LastIndexOf('.') + 1);
            }
            throw new Exception("参数类型匹配失败！");
        }

        /// <summary>
        /// 解析接口
        /// </summary>
        /// <param name="module"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private List<CollectInterfaceClass> FindInterface(string module, JToken[] array)
        {
            List<CollectInterfaceClass> list = new List<CollectInterfaceClass>();
            foreach (var item in array)
            {
                var interfaceName = (item.SelectToken("name") + "").Replace(Environment.NewLine, "");
                var des = (item.SelectToken("description") + "").Replace(Environment.NewLine, "");
                list.Add(new CollectInterfaceClass
                {
                    Name = interfaceName,
                    Description = des,
                    Module = module
                });
            }
            return list;
        }

        private async Task<JObject> GetReponse(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody.FromJsonString<JObject>();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }
        }
    }
}
