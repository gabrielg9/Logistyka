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
        int[,] jednostkowe_koszty_transportu;
        int[,] jednostkowe_koszty_transportu_pomocna;
        int[] popyt;
        int[] podaz;
        int[,] rozwiazanie_bazowe;
        int min_i;
        int min_j;
        int min_element;

        public Form1()
        {
            InitializeComponent();
        }
        

     
        private void Button1_Click(object sender, EventArgs e)
        {
            ilosc_dostawcow = int.Parse(textBox1.Text);
            ilosc_odbiorcow = int.Parse(textBox2.Text);

            jednostkowe_koszty_transportu = new int[ilosc_odbiorcow, ilosc_dostawcow];
            jednostkowe_koszty_transportu_pomocna = new int[ilosc_odbiorcow, ilosc_dostawcow];
            jednostkowe_koszty_transportu[0, 0] = int.Parse(textBox9.Text);
            jednostkowe_koszty_transportu[0, 1] = int.Parse(textBox10.Text);
            jednostkowe_koszty_transportu[0, 2] = int.Parse(textBox11.Text);
            jednostkowe_koszty_transportu[1, 0] = int.Parse(textBox12.Text);
            jednostkowe_koszty_transportu[1, 1] = int.Parse(textBox13.Text);
            jednostkowe_koszty_transportu[1, 2] = int.Parse(textBox14.Text);
            jednostkowe_koszty_transportu[2, 0] = int.Parse(textBox15.Text);
            jednostkowe_koszty_transportu[2, 1] = int.Parse(textBox16.Text);
            jednostkowe_koszty_transportu[2, 2] = int.Parse(textBox17.Text);

            for (int i = 0; i < ilosc_dostawcow; i++)
                for (int j = 0; j < ilosc_odbiorcow; j++)
                    jednostkowe_koszty_transportu_pomocna[i, j] = jednostkowe_koszty_transportu[i, j];

            popyt = new int[ilosc_odbiorcow];
            popyt[0] = int.Parse(textBox3.Text);
            popyt[1] = int.Parse(textBox4.Text);
            popyt[2] = int.Parse(textBox5.Text);

            podaz = new int[ilosc_dostawcow];
            podaz[0] = int.Parse(textBox6.Text);
            podaz[1] = int.Parse(textBox7.Text);
            podaz[2] = int.Parse(textBox8.Text);

            rozwiazanie_bazowe = new int[ilosc_odbiorcow, ilosc_dostawcow];
            min_i = 0;
            min_j = 0;
            min_element = jednostkowe_koszty_transportu[0, 0];
            zagadnienie_transportowe_metoda_min_element_Macierzy();
        }

        private void zagadnienie_transportowe_metoda_min_element_Macierzy()
        {

            for (int k = 0; k <= ilosc_dostawcow * ilosc_odbiorcow - 1; k++)
            {
                for (int i = 0; i < ilosc_dostawcow; i++)
                {
                    for (int j = 0; j < ilosc_odbiorcow; j++)
                    {
                        if (jednostkowe_koszty_transportu[i, j] < min_element)
                        {
                            min_element = jednostkowe_koszty_transportu[i, j];
                            min_i = i;
                            min_j = j;
                        }
                    }
                }
                Console.WriteLine(min_element.ToString());
                Console.WriteLine(min_i.ToString());
                Console.WriteLine(min_j.ToString());
                Console.WriteLine("----------");

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

                min_element *= 100;
                jednostkowe_koszty_transportu[min_i, min_j] *= 100;


            }

            for (int i = 0; i < ilosc_dostawcow; i++)
                for (int j = 0; j < ilosc_odbiorcow; j++)
                    Console.WriteLine((rozwiazanie_bazowe[i, j]).ToString());

            textBox25.Text = rozwiazanie_bazowe[0, 0].ToString();
            textBox26.Text = rozwiazanie_bazowe[0, 1].ToString();
            textBox27.Text = rozwiazanie_bazowe[0, 2].ToString();
            textBox28.Text = rozwiazanie_bazowe[1, 0].ToString();
            textBox29.Text = rozwiazanie_bazowe[1, 1].ToString();
            textBox30.Text = rozwiazanie_bazowe[1, 2].ToString();
            textBox31.Text = rozwiazanie_bazowe[2, 0].ToString();
            textBox32.Text = rozwiazanie_bazowe[2, 1].ToString();
            textBox33.Text = rozwiazanie_bazowe[2, 2].ToString();

            int[] alfa = new int[ilosc_dostawcow];
            alfa[0] = 0;
            alfa[1] = -1000;
            alfa[2] = -1000;

            int[] beta = new int[ilosc_odbiorcow];
            beta[0] = -1000;
            beta[1] = -1000;
            beta[2] = -1000;
            for (int k = 0; k < ilosc_dostawcow * ilosc_odbiorcow; k++)
            {
                for (int i = 0; i < ilosc_dostawcow; i++)
                {
                    for (int j = 0; j < ilosc_odbiorcow; j++)
                    {
                        if (rozwiazanie_bazowe[i, j] != 0)
                        {
                            if (beta[j] != -1000)
                            {
                                alfa[i] = jednostkowe_koszty_transportu[i, j] - beta[j];
                            }
                            else if (alfa[i] != -1000)
                            {
                                beta[j] = jednostkowe_koszty_transportu[i, j] - alfa[i];
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < ilosc_dostawcow; i++)
                alfa[i] /= 100;
            for (int j = 0; j < ilosc_odbiorcow; j++)
                beta[j] /= 100;

            Console.WriteLine(".........");
            for (int i = 0; i < ilosc_dostawcow; i++)
                Console.WriteLine(alfa[i].ToString());
            for (int j = 0; j < ilosc_odbiorcow; j++)
                Console.WriteLine(beta[j].ToString());

            textBox19.Text = alfa[0].ToString();
            textBox20.Text = alfa[1].ToString();
            textBox21.Text = alfa[2].ToString();

            textBox22.Text = beta[0].ToString();
            textBox23.Text = beta[1].ToString();
            textBox24.Text = beta[2].ToString();


            int[,] delta = new int[ilosc_odbiorcow, ilosc_dostawcow];
            for (int i = 0; i < ilosc_dostawcow; i++)
            {
                for (int j = 0; j < ilosc_odbiorcow; j++)
                {
                    if (rozwiazanie_bazowe[i, j] != 0)
                        delta[i, j] = 0;
                    else
                    {
                        delta[i, j] = jednostkowe_koszty_transportu[i, j] / 100 - alfa[i] - beta[j];
                    }
                }
            }

            Console.WriteLine("+++++++++");
            for (int i = 0; i < ilosc_dostawcow; i++)
                for (int j = 0; j < ilosc_odbiorcow; j++)
                    Console.WriteLine(delta[i, j].ToString());

            textBox34.Text = delta[0,0].ToString();
            textBox35.Text = delta[0, 1].ToString();
            textBox36.Text = delta[0, 2].ToString();
            textBox37.Text = delta[1, 0].ToString();
            textBox38.Text = delta[1, 1].ToString();
            textBox39.Text = delta[1, 2].ToString();
            textBox40.Text = delta[2, 0].ToString();
            textBox41.Text = delta[2, 1].ToString();
            textBox42.Text = delta[2, 2].ToString();


            int min_i_delta = 0;
            int min_j_delta = 0;
            int min_delta = delta[0, 0];
            for (int i = 0; i < ilosc_dostawcow; i++)
                for (int j = 0; j < ilosc_odbiorcow; j++)
                    if (delta[i, j] < min_delta)
                    {
                        min_delta = delta[i, j];
                        min_i_delta = i;
                        min_j_delta = j;
                    }

            Console.WriteLine("Minimum z delty");
            Console.WriteLine(min_delta.ToString());
            if (min_delta < 0)
            {
                for (int i = 0; i < ilosc_dostawcow; i++)
                    for (int j = 0; j < ilosc_odbiorcow; j++)
                        jednostkowe_koszty_transportu[i, j] /= 100;
                delta_ujemna(rozwiazanie_bazowe, ilosc_odbiorcow, ilosc_dostawcow, delta, min_delta, min_i_delta, min_j_delta);
            }
            else
            {
                wyswietl_wszystko(jednostkowe_koszty_transportu, rozwiazanie_bazowe, delta, ilosc_odbiorcow, ilosc_dostawcow);
            }
        }

        private void delta_ujemna(int[,] rozwiazanie_Bazowe, int iloscOdbiorcow, int iloscDostawcow, int[,] Delta, int minDelta, int min_iDelta, int min_jDelta)
        {
            for(int i=0; i<iloscDostawcow; i++)
            {
                for(int j=0; j<iloscOdbiorcow; j++)
                {
                    if (min_iDelta == i && Delta[i, j] == 0)
                        break;
                }
            }
        }

        private void wyswietl_wszystko(int[,] jednostkowe_koszty_transportu, int[,] rozwiazanie_bazowe, int[,] delta, int iloscOdbiorcow, int iloscDostawcow)
        {
            //wyswietlenie wszystkiego i policzenie kosztu calkowitego. Pokazanie w okienku tablic
            for (int i = 0; i < iloscDostawcow; i++)
                for (int j = 0; j < iloscOdbiorcow; j++)
                    jednostkowe_koszty_transportu[i, j] /= 100;
            int koszt_calkowity = 0;
            for (int i = 0; i < iloscDostawcow; i++)
                for (int j = 0; j < iloscOdbiorcow; j++)
                    koszt_calkowity += jednostkowe_koszty_transportu[i, j] * rozwiazanie_bazowe[i, j];


            Console.WriteLine("Koszt calkowity");
            Console.WriteLine(koszt_calkowity.ToString());
            textBox18.Text = koszt_calkowity.ToString();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox34_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox36_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox37_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox38_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox39_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox40_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox41_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox42_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox31_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox32_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox27_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox28_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox29_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox31_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox32_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
