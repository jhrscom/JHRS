using AspectInjector.Broker;
using CommonServiceLocator;
using JHRS.Core.Events;
using JHRS.Core.Extensions;
using Prism.Events;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KWT.Core.Aop
{
    /// <summary>
    /// 处理进度
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(WaitCompleteAttribute))]
    public class WaitCompleteAttribute : Attribute
    {
        private static MethodInfo _taskTHandler = typeof(WaitCompleteAttribute).GetMethod(nameof(WaitCompleteAttribute.WrapAsync), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo _taskHandler = typeof(WaitCompleteAttribute).GetMethod(nameof(WaitCompleteAttribute.WrapTaskAsync), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo _normalMethodHandler = typeof(WaitCompleteAttribute).GetMethod(nameof(WaitCompleteAttribute.WrapSync), BindingFlags.NonPublic | BindingFlags.Static);

        private static int delay = 500;

        [Advice(Kind.Around, Targets = Target.Method)]
        public object HandleMethod([Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.ReturnType)] Type retType)
        {
            if (typeof(Task).IsAssignableFrom(retType) && retType.IsGenericType)
            {
                var syncResultType = retType.IsConstructedGenericType ? retType.GenericTypeArguments[0] : typeof(object);
                return _taskTHandler.MakeGenericMethod(syncResultType).Invoke(this, new object[] { target, args });
            }
            else if (typeof(Task).IsAssignableFrom(retType) && retType.IsGenericType == false)
            {
                return _taskHandler.Invoke(this, new object[] { target, args });
            }
            else if (retType.FullName == "System.Void")
            {
                return WrapVoid(target, args);
            }
            else
            {
                return _normalMethodHandler.MakeGenericMethod(retType).Invoke(this, new object[] { target, args });
            }
        }

        private static T WrapSync<T>(Func<object[], object> target, object[] args)
        {
            try
            {
                MaskExtensions.Show();
                Task.Delay(delay);
                return (T)target(args);
            }
            catch (Exception e)
            {
                MessageBox.Show("攔截方法出錯，原因：" + e.Message + "，請檢查被攔截的方法參數是否正確。", "系統提示", (MessageBoxButton)0, (MessageBoxImage)16);
                return default(T);
            }
            finally
            {
                ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ConstrolStateEvent>().Publish(new ControlState { IsEnabled = true });
                MaskExtensions.Close();
            }
        }


        private static async Task WrapTaskAsync(Func<object[], object> target, object[] args)
        {
            try
            {
                MaskExtensions.Show();
                await Task.Delay(delay);
                await (Task)target(args);
            }
            catch (Exception e)
            {
                MessageBox.Show("攔截方法出錯，原因：" + e.Message + "，請檢查被攔截的方法參數是否正確。", "系統提示", (MessageBoxButton)0, (MessageBoxImage)16);
            }
            finally
            {
                ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ConstrolStateEvent>().Publish(new ControlState { IsEnabled = true });
                MaskExtensions.Close();
            }
        }

        private static async Task<T> WrapAsync<T>(Func<object[], object> target, object[] args)
        {
            try
            {
                MaskExtensions.Show();
                await Task.Delay(delay);
                return await (Task<T>)target(args);
            }
            catch (Exception e)
            {
                MessageBox.Show("攔截方法出錯，原因：" + e.Message + "，請檢查被攔截的方法參數是否正確。", "系統提示", (MessageBoxButton)0, (MessageBoxImage)16);
                return default(T);
            }
            finally
            {
                ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ConstrolStateEvent>().Publish(new ControlState { IsEnabled = true });
                MaskExtensions.Close();
            }
        }

        private object WrapVoid(Func<object[], object> target, object[] args)
        {
            try
            {
                MaskExtensions.Show();
                Task.Delay(delay);
                return target(args);
            }
            catch (Exception e)
            {
                MessageBox.Show("攔截方法出錯，原因：" + e.Message + "，請檢查被攔截的方法參數是否正確。", "系統提示", (MessageBoxButton)0, (MessageBoxImage)16);
                return default;
            }
            finally
            {
                ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ConstrolStateEvent>().Publish(new ControlState { IsEnabled = true });
                MaskExtensions.Close();
            }
        }
    }
}