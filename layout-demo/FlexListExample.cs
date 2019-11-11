using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace LayoutDemo
{
    class FlexListExample : Example
    {

        /*
         *  Vertical list (container) of flex layouts (items).
         *  Each flex items has 2 children red and green which are centred and maximum
         *  space between them.
         */

        public FlexListExample() : base( "FlexibleList" )
        {}

        private View itemContainer = null;

        public override void Create()
        {
            LinearLayout linear = new LinearLayout();
            linear.LinearOrientation = LinearLayout.Orientation.Vertical;

            Window.Instance.BackgroundColor = Color.White;
            itemContainer = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Layout = linear,
                BackgroundColor = Color.Yellow,
                Name = "itemContainer",
            };

            for (int i = 0; i < 20; i++)
            {
                CreateItem(itemContainer);
            }

            Window.Instance.GetDefaultLayer().Add(itemContainer);
        }

        public override void Remove()
        {
            Window window = LayoutingExample.GetWindow();
            window.Remove(itemContainer);
            itemContainer = null;
        }

        void CreateItem(View container)
        {
            FlexLayout itemLayout = new FlexLayout();
            itemLayout.Direction = FlexLayout.FlexDirection.Row;
            itemLayout.ItemsAlignment = FlexLayout.AlignmentType.Center;
            itemLayout.Justification = FlexLayout.FlexJustification.SpaceBetween;

            View item = new View();
            item.WidthSpecification = LayoutParamPolicies.MatchParent;
            item.HeightSpecification = LayoutParamPolicies.WrapContent;

            item.Layout = itemLayout;
            item.BackgroundColor = Color.Blue;
            item.Margin = new Extents(0, 0, 20, 20);
            item.Name = "item";
            View child1 = new View()
            {
                BackgroundColor = Color.Green,
                WidthSpecification = 200,
                HeightSpecification = 200,
                Name = "child1",
            };
            TextLabel textLabel1 = new TextLabel()
            {
                Text = "child1",
                Name = "child1-text"
            };


            View child2 = new View()
            {
                WidthSpecification = 100,
                HeightSpecification = 100,
                BackgroundColor = Color.Red,
                Name = "child2",
            };

            TextLabel textLabel2 = new TextLabel();
            textLabel2.Text = "child2";
            textLabel2.Name = "child2-text";

            child1.Add(textLabel1);
            child2.Add(textLabel2);

            item.Add(child1);
            item.Add(child2);

            container.Add(item);
        }
    };
}
