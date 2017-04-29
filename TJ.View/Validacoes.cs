using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TJ.View
{
    public class Validacoes
    {
        public static string caminhoExe()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static bool validarCampos(List<Control> controles)
        {
            bool valido = true;
            for (int i = 0; i < controles.Count; i++)
            {
                if (controles[i] is TextBox)
                {
                    if (!(controles[i] as TextBox).Text.Any())
                    {
                        (controles[i] as TextBox).BorderBrush = new SolidColorBrush(Colors.Red);
                        valido = false;
                    }
                }
                else
                {
                    if (controles[i] is PasswordBox)
                    {
                        if (!(controles[i] as PasswordBox).Password.Any())
                        {
                            (controles[i] as PasswordBox).BorderBrush = new SolidColorBrush(Colors.Red);
                            valido = false;
                        }
                    }
                    else
                    {
                        if (controles[i] is ComboBox)
                        {
                            if ((controles[i] as ComboBox).SelectedIndex == -1)
                            {
                                (controles[i] as ComboBox).BorderBrush = new SolidColorBrush(Colors.Red);
                                valido = false;
                            }
                        }
                        else
                        {
                            if (controles[i] is DatePicker)
                            {
                                if ((controles[i] as DatePicker).SelectedDate == null)
                                {
                                    (controles[i] as DatePicker).BorderBrush = new SolidColorBrush(Colors.Red);
                                    valido = false;
                                }
                            }
                        }
                    }
                }
            }
            return valido;
        }
    }
}
