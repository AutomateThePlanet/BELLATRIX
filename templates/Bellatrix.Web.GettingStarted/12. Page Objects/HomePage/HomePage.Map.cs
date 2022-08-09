namespace Bellatrix.Web.GettingStarted._12._Page_Objects;

public partial class HomePage
{
    public Select SortDropDown => App.Components.CreateByNameEndingWith<Select>("orderby");
    public Anchor ProtonMReadMoreButton => App.Components.CreateByInnerTextContaining<Anchor>("Read more");
    public Anchor AddToCartFalcon9 => App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
    public Anchor ViewCartButton => App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
}