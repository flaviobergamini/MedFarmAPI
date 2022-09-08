namespace MedFarmAPI.Services
{
    public class ViewBodyService
    {
        public string BodyEmail(string name)
        {
            string body = "<meta charset='utf-8'>" + 
                "<div style='background: #0399BA;'>" +
                    "<div style='text-align: center; margin: auto;'>" +
                        "<img src = 'https://raw.githubusercontent.com/flaviobergamini/MedFarmAPI/Image/Logo%20MedFarm_op_50.png' style='width: 70px; text-align:center; margin: auto;'>" +
                    "</div>" +
                    "<div style='height: 25px;'></div>" +
                    "<div style='width: 500px; margin: auto; border: double 2px lightgray; border-radius: 7px; background:lightgray'>" +
                    $"<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm, {name}</h1>" +
                     "<h2 style='text-align:center; font-size:10pt'>Voce criou sua conta com sucesso!!</h2>" +
                    "</div>" +
                    "<div style='height: 25px; '></div>" +
                "</div>";
            return body;
        }

        public string BodyEmailPasswordReset(string name, string token)
        {
            string body = "<meta charset='utf-8'>" +
                "<div style='background: #0399BA;'>" +
                    "<div style='text-align: center; margin: auto;'>" +
                        "<img src = 'https://raw.githubusercontent.com/flaviobergamini/MedFarmAPI/Image/Logo%20MedFarm_op_50.png' style='width: 70px; text-align:center; margin: auto;'>" +
                    "</div>" +
                    "<div style='height: 25px;'></div>" +
                    "<div style='width: 500px; margin: auto; border: double 2px lightgray; border-radius: 7px; background:lightgray'>" +
                        $"<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm, {name}</h1>" +
                         "<h2 style='text-align:center; font-size:10pt'>Voce solicitou um servico para redefinicao de senha.</h2>" +
                         "<h2 style='text-align:center; font-size:10pt'>Clique no botao abaixo para adicionar a nova senha:</h2>" +
                         "<div style='text-align: center; margin: auto;'>" +
                            "<a href='https://localhost:7122/passwordReset.html'>" +
                                "<button style='background: #fae900; border-radius: 20px; padding: 10px; cursor: pointer; font-weight: bold; color: #0399BA; border: none; font-size: 16px;'>" +
                                "Redefinir Senha</button>" +
                            "</a>" + 
                        "</div>" +
                    "</div>" +
                    "<div style='height: 25px; '></div>" +
                "</div>";
            return body;
        }
    }
}
