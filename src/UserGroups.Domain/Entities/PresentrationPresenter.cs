namespace UserGroups.Domain.Entities
{
    public class PresentationPresenter
    {
        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }
        public int? PresenterId { get; set; }
        public Presenter Presenter { get; set; }

        public string PresenterPresentationBody { get; set; }
    }
}