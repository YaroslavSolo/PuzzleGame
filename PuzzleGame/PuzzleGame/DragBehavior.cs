using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PuzzleGame
{
    public class DragBehavior : Behavior<UIElement>
    {
        private Point elementStartPosition;
        private Point mouseStartPosition;
        private TranslateTransform translate = new TranslateTransform();
        private RotateTransform rotate;
        private Point trueTranslate;
        private TransformGroup group = new TransformGroup();

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            AssociatedObject.RenderTransform = group;
            AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);
            group.Children.Add(translate);

            AssociatedObject.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.ClickCount == 2)
                {
                    if (group.Children.Contains(rotate))
                    {
                        group.Children.Remove(rotate);
                        translate.X = trueTranslate.X;
                        translate.Y = trueTranslate.Y;
                    }
                    else
                    {
                        rotate = new RotateTransform(90);                    
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
                    if (group.Children.Contains(rotate))
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
