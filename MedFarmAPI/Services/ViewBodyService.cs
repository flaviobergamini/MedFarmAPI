namespace MedFarmAPI.Services
{
    public class ViewBodyService
    {
        public string BodyEmailClient(string name)
        {
            string body = "<div style='background:lightgray'>" +
                    $"<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm, {name}</h1>" +
                    "<h2 style='text-align:center; font-size:10pt'>Você criou sua conta de Cliente</h2>" +
                    "</div>";
            return body;
        }

        public string BodyEmailDoctor(string name)
        {
            string body = "<div style='background:lightgray'>" +
                    $"<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm, {name}</h1>" +
                    "<h2 style='text-align:center; font-size:10pt'>Você criou sua conta de Cliente</h2>" +
                    "</div>";
            return body;
        }

        public string BodyEmailDrugstore(string name)
        {
            string body = "<div style='background:lightgray'>" +
                    $"<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm, {name}</h1>" +
                    "<h2 style='text-align:center; font-size:10pt'>Você criou sua conta de Cliente</h2>" +
                    "</div>";
            return body;
        }
    }
}
