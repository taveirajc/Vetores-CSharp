using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vetores
{

    public partial class Form1 : Form
    {
        Random randNum = new Random();

        int[] vet = new int[15];  // vetor original
        int[] vet1 = new int[15]; // vetor ordenado
        int[] vet2 = new int[15]; // vetor invertido

        Label[] labels = new Label[15];

        int num, x, vazio = 0;

        public Form1()
        {
            InitializeComponent();
            btn_pesquisar.Enabled = false; // LIBERA O BOTÃO  DE PESQUISA SOMENTE QUANDO O VETOR ESTIVER ORDENADO
            textBox31.Select();            // TEXTBOX DO VALOR INICIAL DOS NÚMEROS A SEREM SORTEADOS
        }

        #region BOTÃO PARA GERAR OS NÚMEROS ALEATÓRIOS SEM REPETIÇÃO
        public void btn_gerar_Click(object sender, EventArgs e)
        {
            limpa_labels(flowLayoutPanel2);
            limpa_labels(flowLayoutPanel3);
            label1.Text = string.Empty;

            int n, n1;
            bool ehUmNumero = int.TryParse(textBox31.Text, out n);
            bool ehUmNumero1 = int.TryParse(textBox32.Text, out n1);

            if (textBox31.Text == string.Empty || textBox32.Text == string.Empty || (textBox31.Text == string.Empty && textBox32.Text == string.Empty))
            {
                System.Console.Beep();
                label1.Text = "Digite o valor inicial e o valor final dos números.";
                textBox31.Select();
            }
            else
            if ((Convert.ToInt32(textBox32.Text) - Convert.ToInt32(textBox31.Text)) < 15)
            {
                System.Console.Beep();
                label1.Text = "Faixa impossível para números sem repetição. Digite um valor maior no segundo número.";
                textBox31.Select();
            }
            else
            if (!ehUmNumero || !ehUmNumero1)
            {
                System.Console.Beep();
                label1.Text = "Digite somente número.";
                textBox31.Select();
            }
            else
            { 
                int vlinicial, vlfinal;

                vlinicial = Convert.ToInt32(textBox31.Text);
                vlfinal = Convert.ToInt32(textBox32.Text);

                Random randNum = new Random();

                // gera os números e preenche os dois vetores vet e vet1
                for (x = 0; x < 15; x++)
                {
                    num = randNum.Next(vlinicial, vlfinal+1); // gera numeros aleatorios entre valores digitados

                    while (verificacao(num) == 1)
                    {
                        num = randNum.Next(vlinicial, vlfinal);
                    }
                    vet[x] = num;
                    vazio = 1;
                }
                Array.Copy(vet, vet1, 15);       // copia o novo vetor vet no vetor vet1
                Array.Copy(vet, vet2, 15);       // copia o novo vetor vet no vetor vet2
                mostrar1(flowLayoutPanel1, vet); // PASSA O CONTROLE E O VETOR COMO ARGUMENTO
                Maior_menor();                  // CALCULA E MOSTRA O MAIOR E MENOR VALOR DO VETOR
                textBox34.Text = vet.Length.ToString();
            }
        }

        // VERIFICA SE O NÚMERO SORTEADO JÁ TEM NO VETOR.
        int verificacao(int num1)
        {
            int index = Array.FindLastIndex(vet, item => item == num1); // verifica se o num1 existe no vetor
            if (index < 0)  // -1 o num1 não existe no vetor
                return 0;
            else
                return 1;
        }
        #endregion

        #region BOTÃO PARA GERAR OS NÚMEROS ALEATÓRIOS COM REPETIÇÃO
        private void btn_gerarcomrepeticao_Click(object sender, EventArgs e)
        {
            limpa_labels(flowLayoutPanel2);
            limpa_labels(flowLayoutPanel3);
            label1.Text = string.Empty;
           
            int n, n1;
            bool ehUmNumero = int.TryParse(textBox31.Text, out n);
            bool ehUmNumero1 = int.TryParse(textBox32.Text, out n1);
          
            if (textBox31.Text == string.Empty || textBox32.Text == string.Empty || (textBox31.Text == string.Empty && textBox32.Text == string.Empty))
            {
                System.Console.Beep();
                label1.Text = "Digite o valor inicial e o valor final dos números.";
                textBox31.Select();
            }
            else
            if (!ehUmNumero || !ehUmNumero1) // VERIFICA SE FOI DIGITADO SOMENTE NÚMERO
            {
                System.Console.Beep();
                label1.Text = "Digite somente número.";
                textBox31.Select();
            }
            else
            {
                int vlinicial, vlfinal;

                vlinicial = Convert.ToInt32(textBox31.Text);
                vlfinal = Convert.ToInt32(textBox32.Text);

                Random randNum = new Random();

                // gera os números e preenche os dois vetores vet e vet1
                for (x = 0; x < 15; x++)
                {
                    num = randNum.Next(vlinicial, vlfinal +1); // gera numeros aleatorios entre valores digitados
                    vet[x] = num;
                    vazio = 1;
                }
                Array.Copy(vet, vet1, 15); // copia o novo vetor vet no vetor vet1
                Array.Copy(vet, vet2, 15);  // copia o novo vetor vet no vetor vet2
                mostrar1(flowLayoutPanel1, vet);   // PASSA O CONTROLE E O VETOR COMO ARGUMENTO
                textBox34.Text = vet.Length.ToString();  //MOSTRA TAMANHO DO VETOR
                Maior_menor(); // CALCULA E MOSTRA O MAIOR E MENOR NÚMERO DO VETOR
            }
        }
        #endregion

        #region LOCALIZA O MENOR,  MAIOR, PARES E ÍMPARES DO VETOR
        private void Maior_menor()
        {
            int maior = vet[0], menor = vet[0], soma = 0, totpares = 0, totimpares = 0;
            for (x = 0; x < 15; x++)
            {
                soma = soma + vet[x]; // calcula a soma acumulada para cálculo da média
                if (vet[x] > maior)
                    maior = vet[x];
                else
                    if (vet[x] < menor)
                    menor = vet[x];
                if (vet[x] % 2 == 0)
                    totpares++;
                else
                    totimpares++;

            }
            textBox1.Text = totpares.ToString();
            textBox2.Text = totimpares.ToString();
            textBox50.Text = maior.ToString();
            textBox51.Text = menor.ToString();
            textBox52.Text = (soma / vet.Length).ToString();  // cálculo da média
        }
        #endregion

        #region ORDENA O VETOR 2 EM ORDEM CRESCENTE
        private void btn_ordenar_Click(object sender, EventArgs e)
        {
            if (vazio == 0)
            {
                label1.Text = "Vetor está vazo.";
                System.Console.Beep();
            }
            else
            {
                // classifica o vetor em ordem crescente
                Array.Sort(vet1);
                mostrar1(flowLayoutPanel2, vet1);
                btn_pesquisar.Enabled = true;
                textBox33.Enabled = true;
            }
        }
        #endregion

        #region MOSTRA VETOR 2 EM ORDEM DECRESCENTE
        private void btn_ord_decrescente_Click(object sender, EventArgs e)
        {
            if (vazio == 0)
            {
                System.Console.Beep();
                label1.Text = "Vetor está vazo.";
            }
            else
            {
                //Array.Sort(vet1);
                //mostrar1(panel2, vet1);
                //btn_pesquisar.Enabled = true

                int[] vetAux = new int[15];

                int contador = 0;
                for (int i = vet1.Length - 1; i >= 0; i--)
                {
                    vetAux[contador] = vet1[i];
                    contador++;
                }
                mostrar1(flowLayoutPanel2, vetAux);
                btn_pesquisar.Enabled = false;
                textBox33.Text = string.Empty;
                textBox33.Enabled = false;
            }
        }
        #endregion

        #region PESQUISAR NO VETOR ORDENADO - VETOR 2
        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            int n;
            bool ehUmNumero = int.TryParse(textBox33.Text, out n);

            if (!ehUmNumero)
            {
                System.Console.Beep();
                label1.Text = "Digite somente número.";
                textBox31.Select();
            }
            else
            if (vazio == 0)
            {
                System.Console.Beep();
                label1.Text = "Vetor está vazio.";
            }
            else
                FindMyObject(vet1, Convert.ToInt32(textBox33.Text));
        }


        // LOCALIZAR ITEM NO VETOR ORDENADO - VETOR 2 - PESQUISA BINARIA
        public void FindMyObject(Array myArr, object myObject)
        {
            int myIndex = Array.BinarySearch(myArr, myObject);
            if (myIndex < 0)
            {
                System.Console.Beep();
                label1.Text = "Número não encontrado no vetor.";
            }
            else
            {
                label1.Text = "Número encontrado na posição " + (myIndex).ToString() + " do vetor.";
            }
        }
        #endregion

        #region INVERTER O VETOR 3
        private void btn_inverte_Click(object sender, EventArgs e)
        {
            if (vazio == 0)
            {
                System.Console.Beep();
                label1.Text = "Vetor está vazo.";
            }
            else
            {
                Array.Reverse(vet2);
                mostrar1(flowLayoutPanel3, vet2);
            }
        }
        #endregion

        #region ZERA TODOS OS VETORES E LIMPA OS TEXTBOX DOS FLOWLAYOUTPANEL
        private void btn_zeravetor_Click(object sender, EventArgs e)
        {
            if (vazio == 0)
            {
                System.Console.Beep();
                label1.Text = "Vetor já está vazio.";
            }
            else
            {
                Array.Clear(vet, 0, vet.Length);  // ZERA OS VETORES
                Array.Clear(vet1, 0, vet1.Length);
                Array.Clear(vet2, 0, vet2.Length);
                limpa_labels(flowLayoutPanel1);               // LIMPA OS LABELS DOS FLOWLAYOUTPANELs
                limpa_labels(flowLayoutPanel2);
                limpa_labels(flowLayoutPanel3);
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox31.Text = string.Empty;  // LIMPA OS TEXTBOX
                textBox32.Text = string.Empty;
                textBox33.Text = string.Empty;
                textBox34.Text = string.Empty;
                textBox50.Text = string.Empty;  
                textBox51.Text = string.Empty;
                textBox52.Text = string.Empty;

                vazio = 0;
            }
            textBox31.Select();
        }
        #endregion

        #region MOSTRA A NUMERAÇÃO SEQUENCIAL DOS TEXTBOX DOS 3 PANELS
        // mostra a numeração sequencial dos textboxs
        private void gera_indices_Click(object sender, EventArgs e)
        {
            mostra_indices(flowLayoutPanel1);
            mostra_indices(flowLayoutPanel2);
            mostra_indices(flowLayoutPanel3);
        }
        #endregion

        #region SAÍDA DO PROGRAMA
        private void btn_sair_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region MOSTRAR OS VETORES
        private void mostrar1(Control c, int[] arr) // RECEBE O CONTROLE E O VETOR - DEFINIDO COMO arr. QUALQUER NOME SERVE.
        {
            if (c is FlowLayoutPanel)
                for (int i = 0; i < c.Controls.Count; i++)
                    c.Controls[i].Text = arr[i].ToString();
        }
        #endregion

        #region ROTINA PARA ALTERAR VALOR NO VETOR
        private void btn_alterar_Click(object sender, EventArgs e)
        {
            if (vazio == 0)
            {
                System.Console.Beep();
                label1.Text = "Vetor já está vazio.";
            }
            else
            { 
                int n, n1;
                bool ehUmNumero = int.TryParse(textBox3.Text, out n);
                bool ehUmNumero1 = int.TryParse(textBox4.Text, out n1);

                if (!ehUmNumero || !ehUmNumero1)
                {
                    System.Console.Beep();
                    label1.Text = "Digite somente número.";
                    textBox3.Select();
                }
                else
                {
                    int valor = Convert.ToInt32(textBox3.Text);
                    if (vet.Contains(valor))  // verifica se o valor se encontra no vetor
                    {
                        int indice = Array.IndexOf(vet, valor);         // localiza o índice do vetor e guarda  na variável indice.
                        vet[indice] = Convert.ToInt32(textBox4.Text);  // coloca o novo número na posição do número anterior no vetor
                        Array.Copy(vet, vet1, 15);                    // copia o novo vetor vet no vetor vet1
                        Array.Copy(vet, vet2, 15);                   // copia o novo vetor vet no vetor vet2
                        mostrar1(flowLayoutPanel1, vet);            // mostra o novo vetor
                        Maior_menor();
                        System.Console.Beep();
                        limpa_labels(flowLayoutPanel2);            // limpa os outros vetores.
                        limpa_labels(flowLayoutPanel3);
                        label1.Text = "Número alterado.";
                    }
                    else
                    {
                        label1.Text = "Número " + valor + " não se encontra no vetor.";
                        System.Console.Beep();
                    }
                }
            }
        }
        #endregion

        #region ROTINA QUE NÃO ACEITA LETRA NOS TEXTBOX
        private void So_numero(object sender, KeyPressEventArgs e)
        {
            //Se a tecla digitada não for número e nem backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                System.Console.Beep();
                //Atribui True no Handled para cancelar o evento
                label1.Text = "Digite somente número.";
            }
        }
        #endregion

        #region MOSTRA INDICE DOS VETORES
        private void mostra_indices(Control c)
        {
            for (int i = 0; i < c.Controls.Count; i++)
            {
                c.Controls[i].Text = i.ToString();
            }
        }
        #endregion

        #region LIMPA TEXTBOX DOS FLOWLAYOUTPANEL
        private void limpa_labels(Control c)
        {
            for (int i = 0; i < c.Controls.Count; i++)
            {
                c.Controls[i].Text = string.Empty;
            }

        }
        #endregion
    }
}
