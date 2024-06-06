using X.PagedList;

namespace Front_5.Models
{
    public class Twomodel
    {
        public List<Sport_pro> Card{ get; set; }
        public List<state> Stat{ get; set; }

        public List<Slider> Slider_L { get; set; }
        public Slider Slider_T { get; set; }
        public Sport_pro Card_t { get; set; }
        public state Stat_t { get; set; }
        public IPagedList<state> PagedStates { get; set; }
        public IPagedList<Sport_pro> PagedCards { get; set; }
    }


}
