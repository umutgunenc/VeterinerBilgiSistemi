namespace VeterinerBilgiSistemi.Models.Classes
{
    public class YapayZekaTahminResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string tahmin { get; set; }
        public int tahminId { get; set; }
    }
}
