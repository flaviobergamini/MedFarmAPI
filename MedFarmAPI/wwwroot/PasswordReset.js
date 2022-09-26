const access = new URLSearchParams(window.location.search);  //Query string, buscando dados da URL
const token = access.get('token');

let message = document.querySelector('#message');
let button = document.querySelector('#btnUpdate');
let nPassword = document.querySelector('#inputPassword');
let cPassword = document.querySelector('#inputConfirmPassword');

let data;

button.addEventListener('click', function (e) {
    e.preventDefault();

    data = {
        token: token,
        newPassword: nPassword.value,
        confirmPassword: cPassword.value
    }

    fetch('https://medfarmapi.azurewebsites.net/v1/password/reset/user', {
    method: 'PATCH',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json;charset=utf-8'
    },
    body: JSON.stringify(
        data
    ),
})
    .then((response) => {
        if (response.ok) {
            msn = '<div class="alert alert-success alert-dismissible fade show">'+
                    '<button class="btn-close" data-bs-dismiss="alert"></button>'+
                    'Senha atualizada com <strong>Sucesso</strong>'+
                  '</div>';
            message.insertAdjacentHTML('afterbegin', msn);
        }
        else {
            msn = '<div class="alert alert-danger alert-dismissible fade show">'+
                    '<button class="btn-close" data-bs-dismiss="alert"></button>'+
                    '<strong>Falha</strong> na atualização da senha'+
                  '</div>';
            message.insertAdjacentHTML('afterbegin', msn);
        }
    })
    .catch(f => {
        msn = '<div class="alert alert-danger alert-dismissible fade show">'+
                    '<button class="btn-close" data-bs-dismiss="alert"></button>'+
                    '<strong>Falha</strong> na atualização da senha'+
                  '</div>';
        message.insertAdjacentHTML('afterbegin', msn);
    })
}); 
