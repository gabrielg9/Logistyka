﻿using System;
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
           zagadnienie_transportowe_metoda_min_element_Macierzy();
        }

        private void zagadnienie_transportowe_metoda_min_element_Macierzy()
        {
            ilosc_dostawcow = int.Parse(textBox1.Text);
            ilosc_odbiorcow = int.Parse(textBox2.Text);

            int[,] jednostkowe_koszty_transportu = new int[ilosc_odbiorcow, ilosc_dostawcow];
            int[,] jednostkowe_koszty_transportu_pomocna = new int[ilosc_odbiorcow, ilosc_dostawcow];
            jednostkowe_koszty_transportu[0, 0] = 3;
            jednostkowe_koszty_transportu[0, 1] = 5;
            jednostkowe_koszty_transportu[0, 2] = 7; 
            jednostkowe_koszty_transportu[1, 0] = 12; 
            jednostkowe_koszty_transportu[1, 1] = 10;
            jednostkowe_koszty_transportu[1, 2] = 9;
            jednostkowe_koszty_transportu[2, 0] = 13; 
            jednostkowe_koszty_transportu[2, 1] = 3; 
            jednostkowe_koszty_transportu[2, 2] = 9;

            for (int i = 0; i < ilosc_dostawcow; i++)
                for (int j = 0; j < ilosc_odbiorcow; j++)
                    jednostkowe_koszty_transportu_pomocna[i, j] = jednostkowe_koszty_transportu[i, j];

            int[] popyt = new int[ilosc_odbiorcow]; 
            popyt[0] = int.Parse(textBox6.Text);//20
            popyt[1] = int.Parse(textBox7.Text);//40
            popyt[2] = int.Parse(textBox8.Text);//90

            int[] podaz = new int[ilosc_dostawcow];
            podaz[0] = int.Parse(textBox3.Text);//50
            podaz[1] = int.Parse(textBox4.Text);//70
            podaz[2] = int.Parse(textBox5.Text);//30

            int[,] rozwiazanie_bazowe = new int[ilosc_odbiorcow, ilosc_dostawcow];
            int min_i = 0;
            int min_j = 0;
            int min_element = jednostkowe_koszty_transportu[0, 0];
            
            

            for(int k=0; k <= ilosc_dostawcow*ilosc_odbiorcow-1; k++)
            {
                for (int i=0; i<ilosc_dostawcow; i++)
                {
                    for(int j=0; j<ilosc_odbiorcow; j++)
                    {
                        if(jednostkowe_koszty_transportu[i,j] < min_element )
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

            int[] alfa = new int[ilosc_dostawcow];
            alfa[0] = 0;
            alfa[1] = -1000;
            alfa[2] = -1000;

            int[] beta = new int[ilosc_odbiorcow];
            beta[0] = -1000;
            beta[1] = -1000;
            beta[2] = -1000;
            for(int k=0; k<ilosc_dostawcow * ilosc_odbiorcow; k++)
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
            

            int[,] delta = new int[ilosc_odbiorcow, ilosc_dostawcow];
            for(int i=0; i<ilosc_dostawcow; i++)
            {
                for(int j=0; j<ilosc_odbiorcow; j++)
                {
                    if (rozwiazanie_bazowe[i, j] != 0)
                        delta[i, j] = 0;
                    else
                    {
                        delta[i, j] = rozwiazanie_bazowe[i, j] - alfa[i] - beta[j];
                    }
                }
            }
            
               
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
