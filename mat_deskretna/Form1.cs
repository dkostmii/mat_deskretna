using DynamicExpresso;
using mat_deskretna.Extensions;
using mat_deskretna.ValueObjects;
using System;
using System.Data;
using System.Windows.Forms;

namespace mat_deskretna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateAndBindTruthTable(BooleanExpression expr, DataGridView gridView)
        {
            var interpreter = new Interpreter();

            if (expr.Parameters.Length != 3)
            {
                throw new Exception("Expected expression with 3 parameters. For example, \"A OR B AND C\".");
            }

            // создаем новую таблицу
            DataTable table = new DataTable();

            // добавляем столбцы для переменных и результата
            table.Columns.Add(expr.Parameters[0], typeof(bool));
            table.Columns.Add(expr.Parameters[1], typeof(bool));
            table.Columns.Add(expr.Parameters[2], typeof(bool));
            table.Columns.Add("Result", typeof(bool));

            // настраиваем DataGridView
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // создаем столбцы для отображения данных
            DataGridViewTextBoxColumn columnA = new DataGridViewTextBoxColumn();
            columnA.DataPropertyName = expr.Parameters[0];
            columnA.HeaderText = expr.Parameters[0];

            DataGridViewTextBoxColumn columnB = new DataGridViewTextBoxColumn();
            columnB.DataPropertyName = expr.Parameters[1];
            columnB.HeaderText = expr.Parameters[1];

            DataGridViewTextBoxColumn columnC = new DataGridViewTextBoxColumn();
            columnC.DataPropertyName = expr.Parameters[2];
            columnC.HeaderText = expr.Parameters[2];

            DataGridViewTextBoxColumn columnResult = new DataGridViewTextBoxColumn();
            columnResult.DataPropertyName = "Result";
            columnResult.HeaderText = "Result";

            gridView.Columns.Clear();

            // добавляем столбцы в DataGridView
            gridView.Columns.Add(columnA);
            gridView.Columns.Add(columnB);
            gridView.Columns.Add(columnC);
            gridView.Columns.Add(columnResult);

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
                    new Parameter(expr.Parameters[0], a),
                    new Parameter(expr.Parameters[1], b),
                    new Parameter(expr.Parameters[2], c)
                });

                // добавляем строку с результатом в таблицу
                table.Rows.Add(a, b, c, result);
            }

            gridView.DataSource = table;
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

                p_label.Text = expr.Parameters[0] + " = " + sentence.Parameters[0];
                q_label.Text = expr.Parameters[1] + " = " + sentence.Parameters[1];
                r_label.Text = expr.Parameters[2] + " = " + sentence.Parameters[2];

                rezalt_text.Text = expr.Transformed;

                rezalt_panal.Visible = true;
                panal_p_q_r.Visible = true;
                pictureBox1.Visible = true;

                CreateAndBindTruthTable(expr, dataGridView1);
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
            // создаем таблицу логического уравнения
            var expr = BooleanExpression.From("A AND B OR NOT C");
            CreateAndBindTruthTable(expr, dataGridView1);
        }
    }
}
