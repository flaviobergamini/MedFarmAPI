const access = new URLSearchParams(window.location.search);  //Query string, buscando dados da URL
const token = access.get('token');
//console.log(token);

let message = document.getElementById('#message');
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

    fetch('https://localhost:7122/v1/password/reset/client', {
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
            console.log("Ok");
        }
        else {
            console.log(response.json);
        }
    })
    .catch(f => console.log("Erro", f))

    console.log(data);

}); 
/*
// Realizando requisição PATCH com o endpoint da API para redefinição de senha
fetch( 'https://localhost:7122/v1/password/reset/client', {
    method: 'PATCH',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json;charset=utf-8'
    },
    body: JSON.stringify( data )
} )
.then( function( response ){
    if( response.status != 200 ){
        this.fetchError = response.status;
    }else{
        response.json().then( function( data ){
            this.fetchResponse = data;
        }.bind(this));
    }
}.bind(this));


fetch('https://localhost:7122/v1/password/reset/client', {
    method: 'PATCH',
    body: JSON.stringify(

    ),
    headers: {
        'Content-type': 'application/json; charset=UTF-8',
    },
})
    .then((response) => {
        if (response.ok) {
            console.log("Ok");
        }
        else {
            console.log(response.json);
        }
    })
    .catch(f => console.log("Erro", f))
    */

