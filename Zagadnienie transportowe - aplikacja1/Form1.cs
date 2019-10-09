using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zagadnienie_transportowe___aplikacja1
{
    public partial class Form1 : Form
    {
        private int ilosc_dostawcow;
        private int ilosc_odbiorcow;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            zagadnienie_transportowe_metoda_wierzcholka_NW();
        }

        private void zagadnienie_transportowe_metoda_wierzcholka_NW()
        {
            ilosc_dostawcow = int.Parse(textBox1.Text);
            ilosc_odbiorcow = int.Parse(textBox2.Text);

            int[,] jednostkowe_koszty_transportu = new int[ilosc_odbiorcow, ilosc_dostawcow];
            jednostkowe_koszty_transportu[0, 0] = 3;
            jednostkowe_koszty_transportu[0, 1] = 5;
            jednostkowe_koszty_transportu[0, 2] = 7; 
            jednostkowe_koszty_transportu[1, 0] = 12; 
            jednostkowe_koszty_transportu[1, 1] = 10;
            jednostkowe_koszty_transportu[1, 2] = 9;
            jednostkowe_koszty_transportu[2, 0] = 13; 
            jednostkowe_koszty_transportu[2, 1] = 3; 
            jednostkowe_koszty_transportu[2, 2] = 9; 

            int[] popyt = new int[ilosc_odbiorcow]; 
            popyt[0] = 20;
            popyt[1] = 40;
            popyt[1] = 90;
            int[] podaz = new int[ilosc_dostawcow];
            podaz[0] = 50;
            podaz[1] = 70;
            podaz[2] = 30;

            int[,] rozwiazanie_bazowe = new int[ilosc_odbiorcow, ilosc_dostawcow];
            int min_i = 0;
            int min_j = 0;
            int min_i_poprzednie = 0;
            int min_j_poprzednie = 0;
            int min_element = jednostkowe_koszty_transportu[0, 0];
            

            for(int k=0; ilosc_dostawcow*ilosc_odbiorcow-1; k++)
            {
                for (int i=0; i<ilosc_dostawcow; i++)
                {
                    for(int j=0; j<ilosc_odbiorcow; j++)
                    {
                        if(jednostkowe_koszty_transportu[i,j] < min_element && (min_i_poprzednie != min_i || min_j_poprzednie != min_j))
                        {
                            min_element = jednostkowe_koszty_transportu[i, j];
                            min_i = i;
                            min_j = j; 
                        }
                    }
                }

                if (popyt[min_j] < podaz[min_i])
                {
                    rozwiazanie_bazowe[min_i, min_j] = popyt[min_j];
                    podaz[min_i] = podaz[min_i] - popyt[min_j];
                    popyt[min_j] = 0;
                }
                else
                {
                    rozwiazanie_bazowe[min_i, min_j] = podaz[min_i];
                    popyt[min_j] = popyt[min_j] - podaz[min_i];
                    podaz[min_i] = 0;
                }
                min_i_poprzednie = min_i;
                min_j_poprzednie = min_j;
                
            }
                





        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
