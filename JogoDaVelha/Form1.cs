using System;
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
        private readonly List<Button> _btns = new List<Button>();
        private Color _corAntiga;
        private string _jogada;
        private int _jogadas = 0;
        private bool _vez = true;
        private readonly int[] _vitorias = new int[2];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in Controls)
                if (item is Button btn)
                    _btns.Add(btn);

            _btns.Reverse();
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

        private void ColocarBloco(Button btn)
        {
            if (String.IsNullOrEmpty(btn.Text))
            {
                _jogada = ChecarVez();

                btn.Text = _jogada;
                ChecarJogada();

                _jogadas++;
                _vez = !_vez;
            }
        }

        private string ChecarVez()
            => _vez ? "X" : "O";
        
        private void ChecarJogada()
        {
            if (ChecarDirecoes())
                AnunciarVencedor(_jogada);
            else if (_jogadas == 9)
            {
                MessageBox.Show("Ocorreu um empate.");
                ReiniciarJogo();
            }
        }
            
        private bool ChecarDirecoes()
           => ChecarHorizontais() || ChecarVerticais() || ChecarDiagonais();
        
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
            => button1.Text == _jogada && button5.Text == _jogada && button9.Text == _jogada ||
               button3.Text == _jogada && button5.Text == _jogada && button7.Text == _jogada;
        
        private void AnunciarVencedor(string vencedor)
        {
            ContarVitorias(vencedor);
            MessageBox.Show($"O vencedor é o {vencedor}.");
            ReiniciarJogo();
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
    }
}
