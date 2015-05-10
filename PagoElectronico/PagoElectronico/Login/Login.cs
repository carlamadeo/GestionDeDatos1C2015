using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PagoElectronico.Login
{
    class Login
    {
        public static void procLogin(string user, string password)
        {
            //Compruebo que el usuario no este bloqueado
            if (LoginDB.notBlockedUser(user))
            {
                //Compruebo que el nombre de usuario y contrasenia sean correctos
                if (LoginDB.isSuccessLogin(user, password))
                {
                    //Si ingreso correctamente reseteo la cantidad de logins incorrectos
                    LoginDB.ResetLogin(user);

                    //Si tiene mas de un rol muestro form para elegir el rol
                    if (LoginDB.CountRol(user) > 1)
                    {
                        FormSelectRol formSR = new FormSelectRol();
                        FormLogin.ActiveForm.Hide();
                        formSR.ShowDialog();
                    }

                    else
                    {
                        //TODO Hacer que vaya directo al Rol que tiene
                    }
                }

                else
                {
                    Mensajes.error("Por favor verifique los datos ingresados.");

                    //Si ingreso mal los datos aumento la cantidad de login fallidos
                    LoginDB.WrongLogin(user);

                    //Si la cantidad de logins fallidos es = 3 inhabilito al usuario
                    if (LoginDB.CountWrongLogin(user) == 3)
                    {
                        LoginDB.BlockUser(user);
                    }
                    
                }
            }

            else
            {
                Mensajes.error("El usuario se encuentra Bloqueado. Por favor contacte al administrador.");
            }
            
        }
    }
}
