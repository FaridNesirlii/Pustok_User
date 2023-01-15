namespace Pustok.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly PustokContext _pustokContext;

        public HeaderViewComponent(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            Book book =_pustokContext.books.FirstOrDefault();
            return View(await Task.FromResult(book));   
        }
    }
}
