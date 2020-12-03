namespace Bellatrix.Web.Tests
{
    public partial class HomePage
    {
        public Select SortDropDown => Element.CreateByNameEndingWith<Select>("orderby");
        public Anchor ProtonMReadMoreButton => Element.CreateByInnerTextContaining<Anchor>("Read more");
        public Anchor AddToCartFalcon9 => Element.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        public Anchor ViewCartButton => Element.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
    }
}