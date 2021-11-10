using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TH.Utilities
{
    public static class PaginaEncuesta
    {
        public static string GetPagina()
        {
            string pagina = "<!DOCTYPE html> " +
                "<html> " +
                "<body> " +
                "    <style> " +
                "form { " +
                "  margin: 0 auto; " +
                "  width: 400px; " +
                "  padding: 1em; " +
                "  border: 1px solid #CCC; " +
                "  border-radius: 1em; " +
                "} " +
                " " +
                "ul { " +
                "  list-style: none; " +
                "  padding: 0; " +
                "  margin: 0; " +
                "} " +
                " " +
                "form li + li { " +
                "  margin-top: 1em; " +
                "} " +
                " " +
                "label { " +
                " " +
                "  display: inline-block; " +
                "  width: 90px; " +
                "  text-align: right; " +
                "} " +
                " " +
                "input, " +
                "textarea { " +
                " " +
                "  font: 1em sans-serif; " +
                " " +
                " " +
                "  width: 300px; " +
                "  box-sizing: border-box; " +
                " " +
                " " +
                "  border: 1px solid #999; " +
                "} " +
                " " +
                "input:focus, " +
                "textarea:focus { " +
                " " +
                "  border-color: #000; " +
                "} " +
                " " +
                "textarea { " +
                " " +
                "  vertical-align: top; " +
                " " +
                "  height: 5em; " +
                "} " +
                " " +
                ".button { " +
                " " +
                "  padding-left: 90px;  " +
                "} " +
                " " +
                "button { " +
                " " +
                "  margin-left: .5em; " +
                "} " +
                "    </style> " +
                "    <form action='' method='post'> " +
                "        <ul> " +
                "         <li> " +
                "           <label for='name'>Nombre:</label> " +
                "           <input type='text' id='name' name='user_name'> " +
                "         </li> " +
                "         <li> " +
                "           <label for='mail'>Correo electrónico:</label> " +
                "           <input type='email' id='mail' name='user_mail'> " +
                "         </li> " +
                "         <li> " +
                "           <label for='msg'>Mensaje:</label> " +
                "           <textarea id='msg' name='user_message'></textarea> " +
                "         </li> " +
                "        </ul> " +
                "       </form> " +
                " " +
                "</body> " +
                "</html>  ";

            return pagina;
        }
    }
}
