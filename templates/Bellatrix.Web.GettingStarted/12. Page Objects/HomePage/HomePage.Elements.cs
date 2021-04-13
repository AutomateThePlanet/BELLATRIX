namespace Bellatrix.Web.GettingStarted._12._Page_Objects
{
    public partial class HomePage
    {
        public Select SortDropDown => ElementCreateService.CreateByNameEndingWith<Select>("orderby");
        public Anchor ProtonMReadMoreButton => ElementCreateService.CreateByInnerTextContaining<Anchor>("Read more");
        public Anchor AddToCartFalcon9 => ElementCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        public Anchor ViewCartButton => ElementCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
    }
}