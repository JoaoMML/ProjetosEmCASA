﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaixaEletronico
{
    public partial class Form1 : Form
    {
        Conta[] contas;
        private int quantidadeDeContas;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contas = new Conta[3];

            Cliente c1 = new Cliente();
            contas[0] = new ContaPoupanca();
            contas[0].Titular = c1;
            contas[0].Titular.Nome = "João Marcos";
            contas[0].Numero = 1;
            contas[0].Titular.Idade = 18;
            contas[0].TipoConta = 1;
            contas[0].Deposita(10000);

            Cliente c2 = new Cliente();
            contas[1] = new ContaPoupanca();
            contas[1].Titular = c2;
            contas[1].Titular.Nome = "Victor";
            contas[1].Numero = 2;
            contas[1].Titular.Idade = 18;
            contas[1].TipoConta = 1;
            contas[1].Deposita(2500);

            Cliente c3 = new Cliente();
            contas[2] = new ContaPoupanca();
            contas[2].Titular = c3;
            contas[2].Titular.Nome = "Henrique";
            contas[2].Numero = 3;
            contas[2].Titular.Idade = 17;
            contas[2].TipoConta = 1;
            contas[2].Deposita(8000);

            foreach (Conta conta in contas)
            {
                comboContas.Items.Add(conta.Titular.Nome);
                comboTransferencia.Items.Add(conta.Titular.Nome);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string textoDoValorDoDeposito = textoValor.Text;
            double valorDeposito = Convert.ToDouble(textoDoValorDoDeposito);
            Conta contaSelecionada = this.BuscaContaSelecionada();
            try
            {
                contaSelecionada.Deposita(valorDeposito);
                MessageBox.Show("Deposito de R$ " + valorDeposito + " realizado com sucesso !!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Por Favor informe um valor válido");
            }
            this.MostrarConta(contaSelecionada);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string textoDoValorDoSaque = textoValor.Text;
                double valorSaque = Convert.ToDouble(textoDoValorDoSaque);
                Conta contaSelecionada = this.BuscaContaSelecionada();

                try
                {
                    contaSelecionada.Saca(valorSaque);
                    MessageBox.Show("Saque de R$ " + valorSaque + " realizado com sucesso !!");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Valores negativos são invalidos. ");
                }
                catch (SaldoInsuficienteException)
                {
                    MessageBox.Show("Saldo Insuficiente. ");
                }
                catch (LimiteMenorIdadeException)
                {
                    MessageBox.Show("O seu limite de saque é de R$200,00");
                }
                this.MostrarConta(contaSelecionada);
            }

        }

        private void MostrarConta(Conta conta)
        {
            textoTitular.Text = conta.Titular.Nome;
            textSaldo.Text = Convert.ToString("R$ " + conta.Saldo);
            textN.Text = Convert.ToString(conta.Numero);
        }

        private Conta BuscaContaSelecionada()
        {
            int indiceSelecionado = comboContas.SelectedIndex;
            return this.contas[indiceSelecionado];
        }

        private void textoTitular_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboContas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indiceSelecionado = comboContas.SelectedIndex;
            Conta contaSelecionada = contas[indiceSelecionado];

            textoTitular.Text = contaSelecionada.Titular.Nome;
            textN.Text = Convert.ToString(contaSelecionada.Numero);
            textSaldo.Text = Convert.ToString("R$ " + contaSelecionada.Saldo);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string titularSelecionado = comboContas.Text;
            Conta contaSelecionada = this.BuscaContaSelecionada();
            this.MostrarConta(contaSelecionada);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Conta contaSelecionada = this.BuscaContaSelecionada();

            int indiceDaContaDestino = comboTransferencia.SelectedIndex;

            Conta contaDestino = this.contas[indiceDaContaDestino];
            try
            {
                string textoValorTrans = textoValor.Text;
                double ValorT = Convert.ToDouble(textoValorTrans);


                contaSelecionada.Transfere(contaDestino, ValorT);
                MessageBox.Show("Transferencia realizada com sucesso!!");
            }
            catch (SaldoInsuficienteException)
            {
                MessageBox.Show("O limite é de 200,00 para menores de 18 anos.");
            }
            this.MostrarConta(contaSelecionada);
        }

       public void AdicionaConta(Conta conta)
       {
         this.contas[this.quantidadeDeContas] = conta;
         this.quantidadeDeContas++;
         comboContas.Items.Add(conta);
       }

        private void button4_Click(object sender, EventArgs e)
        {
            CadastroDeContas cadastro = new CadastroDeContas(this);
            cadastro.ShowDialog();
        }
    }
}