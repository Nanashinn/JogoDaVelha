﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaVelha
{
    public partial class Form1 : Form
    {
        private bool _vez = true;
        private int _jogadas = 0;
        private readonly int[] _vitorias = new int[2];
        private Color _corAntiga;
        private readonly List<Button> _btns = new List<Button>();
        private string _jogada;

        public Form1()
        {
            InitializeComponent();
        }

        //true = jogador 1 X
        //false = jogador 2 O
        private string ChecarVez()
        {
            if (_vez)
                return "X";

            return "O";
        }

        private void ColocarBloco(Button btn)
        {
            if (btn.Text != "")
                return;

            _jogada = ChecarVez();

            btn.Text = _jogada;
            ChecarJogada();

            _jogadas++;
            _vez = !_vez;
        }

        private void ChecarJogada()
        {
            if (ChecarDirecoes())
            {
                AnunciarVencedor(_jogada);
                return;
            }

            if (_jogadas == 9)
            {
                MessageBox.Show("Ocorreu um empate.");
                ReiniciarJogo();
            }
        }

        private bool ChecarDirecoes()
        {
            if (ChecarHorizontais() || ChecarVerticais() || ChecarDiagonais())
                return true;

            return false;
        }

        private bool ChecarHorizontais()
        {
            int soma = 1;
            for (int i = 0; i < 3; i++)
            {
                if (_btns[(i + soma) - 1].Text == _jogada && _btns[(i + (soma + 1)) - 1].Text == _jogada && _btns[(i + (soma + 2)) - 1].Text == _jogada)
                    return true;
                soma += 2;
            }

            return false;
        }

        private bool ChecarVerticais()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_btns[(i + 1) - 1].Text == _jogada && _btns[(i + 4) - 1].Text == _jogada && _btns[(i + 7) - 1].Text == _jogada)
                    return true;
            }

            return false;
        }
        
        private bool ChecarDiagonais()
        {
            if (button1.Text == _jogada && button5.Text == _jogada && button9.Text == _jogada) return true;
            else if (button3.Text == _jogada && button5.Text == _jogada && button7.Text == _jogada) return true;

            return false;
        }

        private void ContarVitorias(string vencedor)
        {
            switch (vencedor)
            {
                case "X":
                    _vitorias[0]++;
                    break;
                case "O":
                    _vitorias[1]++;
                    break;
            }

            AtualizarContadorVitorias();
        }

        private void AnunciarVencedor(string vencedor)
        {
            ContarVitorias(vencedor);
            MessageBox.Show($"O vencedor é o {vencedor}.");
            ReiniciarJogo();
        }

        private void AtualizarContadorVitorias()
        {
            label1.Text = $"Vitórias do X: {_vitorias[0]}";
            label2.Text = $"Vitórias do O: {_vitorias[1]}";
        }

        private void ReiniciarJogo()
        {
            foreach (var btn in _btns)
            {
                btn.Text = "";
                btn.BackColor = SystemColors.Control;
            }
            _jogadas = 0;
            _vez = true;
        }

        private void button_enter(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            _corAntiga = btn.BackColor;
            btn.BackColor = Color.AliceBlue;
        }

        private void button_leave(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.BackColor = _corAntiga;
        }

        private void button_click(object sender, EventArgs e) 
            => ColocarBloco((Button)sender);

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in Controls)
            {
                if (item is Button btn)
                {
                    _btns.Add(btn);
                }
            }
            _btns.Reverse();
        }
    }
}
