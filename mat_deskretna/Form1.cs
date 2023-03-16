using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mat_deskretna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DataTable CreateTruthTable(BooleanExpression expr)
        {
            var interpreter = new Interpreter();

            if (expr.Parameters.Length != 3)
            {
                throw new Exception("Expected expression with 3 parameters. For example, \"A OR B AND C\".");
            }

            // создаем новую таблицу
            DataTable table = new DataTable();

            // добавляем столбцы для переменных и результата
            table.Columns.Add("A", typeof(bool));
            table.Columns.Add("B", typeof(bool));
            table.Columns.Add("C", typeof(bool));
            table.Columns.Add("Result", typeof(bool));

            // создаем массив значений для всех возможных комбинаций переменных
            int numRows = 8;
            bool[,] values = new bool[numRows, 3];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int k = (int)Math.Pow(2, j);
                    values[i, j] = ((i / k) % 2 == 0) ? false : true;
                }
            }

            // вычисляем результат для каждой комбинации переменных
            foreach (var row in values.GetAllRows())
            {
                bool a = row[0];
                bool b = row[1];
                bool c = row[2];
                bool result;

                // вычисляем логическое выражение
                result = interpreter.Eval<bool>(expr.Transformed, new[]
                {
                    new Parameter("A", a),
                    new Parameter("B", b),
                    new Parameter("C", c)
                });

                // добавляем строку с результатом в таблицу
                table.Rows.Add(a, b, c, result);
            }

            return table;
        }

        private void Go_button_Click(object sender, EventArgs e)
        {
            try
            {
                var sentence = BooleanSentence.From(text.Text);

                if (sentence.Parameters.Length != 3)
                {
                    throw new Exception("Expected sentence with 3 parameters. For example, \"a lub b i c\".");
                }

                var expr = BooleanExpression.From(sentence.Transformed);
                var interpreter = new Interpreter();

                p_label.Text = "p = " + sentence.Parameters[0];
                q_label.Text = "q = " + sentence.Parameters[1];
                r_label.Text = "r = " + sentence.Parameters[2];

                rezalt_panal.Visible = true;
                panal_p_q_r.Visible = true;
                pictureBox1.Visible = true;

                bool A = true; // значение первой переменной
                bool B = false; // значение второй переменной
                bool C = true; // значение третьей переменной
                bool result; // результат логического уравнения

                // создание таблицы и заполнение ее данными
                DataTable table = new DataTable();
                table.Columns.Add("A", typeof(bool));
                table.Columns.Add("B", typeof(bool));
                table.Columns.Add("C", typeof(bool));
                table.Columns.Add("Result", typeof(bool));

                // вычисление значения логического уравнения для всех комбинаций значений переменных A, B, и C
                for (int i = 0; i < 8; i++)
                {
                    A = (i & 4) != 0;
                    B = (i & 2) != 0;
                    C = (i & 1) != 0;
                    //result_ = A && B || !C;

                    result = interpreter.Eval<bool>(expr.Transformed, new[]
                    {
                        new Parameter(expr.Parameters[0], A),
                        new Parameter(expr.Parameters[1], B),
                        new Parameter(expr.Parameters[2], C),
                    });

                    table.Rows.Add(A, B, C, result);
                }

                // вывод таблицы в DataGridView
                dataGridView1.DataSource = table;
            }
            catch
            {
                rezalt_panal.Visible = false;
                panal_p_q_r.Visible = false;
                pictureBox1.Visible = false;

                MessageBox.Show("Coś jest nie tak z tekstem, sprawdź go.", "Error!");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // настраиваем DataGridView
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // создаем столбцы для отображения данных
            DataGridViewTextBoxColumn columnA = new DataGridViewTextBoxColumn();
            columnA.DataPropertyName = "A";
            columnA.HeaderText = "A";

            DataGridViewTextBoxColumn columnB = new DataGridViewTextBoxColumn();
            columnB.DataPropertyName = "B";
            columnB.HeaderText = "B";

            DataGridViewTextBoxColumn columnC = new DataGridViewTextBoxColumn();
            columnC.DataPropertyName = "C";
            columnC.HeaderText = "C";

            DataGridViewTextBoxColumn columnResult = new DataGridViewTextBoxColumn();
            columnResult.DataPropertyName = "Result";
            columnResult.HeaderText = "Result";

            // добавляем столбцы в DataGridView
            dataGridView1.Columns.Add(columnA);
            dataGridView1.Columns.Add(columnB);
            dataGridView1.Columns.Add(columnC);
            dataGridView1.Columns.Add(columnResult);

            // создаем таблицу логического уравнения
            var expr = BooleanExpression.From("A AND B OR NOT C");
            DataTable table = CreateTruthTable(expr);

            // привязываем таблицу к DataGridView
            dataGridView1.DataSource = table;
        }

    }
}
