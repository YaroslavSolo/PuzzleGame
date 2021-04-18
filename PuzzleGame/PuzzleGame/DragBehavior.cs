using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PuzzleGame
{
    public class DragBehavior : Behavior<UIElement>
    {
        private Point elementStartPosition;
        private Point mouseStartPosition;
        private Point trueTranslate;
        private RotateTransform rotate = new RotateTransform(90);
        private TranslateTransform translate = new TranslateTransform();    
        private TransformGroup group = new TransformGroup();

        public bool Horizontal
        {
            get
            {
                foreach (var el in group.Children)
                {
                    if (el is RotateTransform)
                        return true;
                }

                return false;
            }
        }

        public void FireEvent(object onMe, string invokeMe, params object[] eventParams)
        {
            TypeInfo typeInfo = onMe.GetType().GetTypeInfo();
            FieldInfo fieldInfo = typeInfo.GetDeclaredField(invokeMe);
            MulticastDelegate eventDelagate = (MulticastDelegate)fieldInfo.GetValue(onMe);

            Delegate[] delegates = eventDelagate.GetInvocationList();

            foreach (Delegate dlg in delegates)
            {
                dlg.GetMethodInfo().Invoke(dlg.Target, eventParams);
            }

            //FireEvent(AssociatedObject, "MouseLeftButtonDown", null, EventArgs.Empty);
        }

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            group.Children.Add(AssociatedObject.RenderTransform);
            AssociatedObject.RenderTransform = group;
            AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);
            group.Children.Add(translate);

            AssociatedObject.MouseLeftButtonDown += (sender, e) =>
            {
                if (e == EventArgs.Empty || e.ClickCount == 2)
                {
                    if (Horizontal)
                    {
                        foreach (var el in group.Children)
                        {
                            if (el is RotateTransform)
                            {
                                group.Children.Remove(el);
                                break;
                            }
                        }

                        translate.X = trueTranslate.X;
                        translate.Y = trueTranslate.Y;
                    }
                    else
                    {                     
                        rotate.CenterX = elementStartPosition.X;
                        rotate.CenterY = elementStartPosition.Y;
                        group.Children.Add(rotate);
                    }
                }
                else
                {
                    mouseStartPosition = parent.PointToScreen(e.GetPosition(parent));
                    AssociatedObject.CaptureMouse();
                }  
            };

            AssociatedObject.MouseLeftButtonUp += (sender, e) =>
            {
                AssociatedObject.ReleaseMouseCapture();
                elementStartPosition.X = translate.X;
                elementStartPosition.Y = translate.Y;
            };

            AssociatedObject.MouseMove += (sender, e) =>
            {
                var parentPos = parent.PointToScreen(e.GetPosition(parent));
                Vector diff = (parentPos - mouseStartPosition);
                if (AssociatedObject.IsMouseCaptured)
                {
                    if (Horizontal)
                    {
                        translate.X = elementStartPosition.X + diff.Y / 1.25;
                        translate.Y = elementStartPosition.Y - diff.X / 1.25;
                        trueTranslate.X = elementStartPosition.X + diff.X / 1.25;
                        trueTranslate.Y = elementStartPosition.Y + diff.Y / 1.25;
                    }
                    else
                    {
                        translate.X = elementStartPosition.X + diff.X / 1.25;
                        translate.Y = elementStartPosition.Y + diff.Y / 1.25;
                        trueTranslate.X = elementStartPosition.X + diff.X / 1.25;
                        trueTranslate.Y = elementStartPosition.Y + diff.Y / 1.25;
                    }           
                }
            };
        }
    }
}
