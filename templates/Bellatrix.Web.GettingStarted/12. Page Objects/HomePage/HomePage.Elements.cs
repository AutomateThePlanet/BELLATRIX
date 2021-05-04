namespace Bellatrix.Web.GettingStarted._12._Page_Objects
{
    public partial class HomePage
    {
        public Select SortDropDown => ComponentCreateService.CreateByNameEndingWith<Select>("orderby");
        public Anchor ProtonMReadMoreButton => ComponentCreateService.CreateByInnerTextContaining<Anchor>("Read more");
        public Anchor AddToCartFalcon9 => ComponentCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        public Anchor ViewCartButton => ComponentCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
    }
}