
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        private MarkType[] mResults; // Hold the current results of cells in the active game

        private bool mPlayer1turn; // True if it's player 1 (X) turn or false if it's player 2 turn (0)
        private bool mGameEnded; // True if the game has ended

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        private void NewGame()
        {

            // Create a new blank array of free cells
            mResults = new MarkType[9];
            for (var i = 0; i <mResults.Length;i++)
            {
                mResults[i] = MarkType.Free;
            }
            // Make sure Player 1 start the game
            mPlayer1turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            // Make sure the game hasn't ended
            mGameEnded = false; 
        }

        /// <summary>
        /// Handles a button Click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e"> the events of the click</param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);


            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which player turn it is
            mResults[index] = mPlayer1turn ? MarkType.Cross : MarkType.Nought;

            // Set the button text to the result
            button.Content = mPlayer1turn ? "X" : "O";

            // Change noughts to green

            if (!mPlayer1turn)
                button.Foreground = Brushes.Green;

            // Toggle the players turns
            mPlayer1turn = !mPlayer1turn;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        ///  Check if there is a winner of 3 line Straight
        /// </summary>

        private void CheckForWinner()
        {

            // Check for horizontal wins

            // -- Row 0

            if (mResults[0] !=MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])

            {
                // Game ends

                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Yellow;
            }

            // -- Row 1

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])

            {
                // Game ends

                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Yellow;
            }



            // Continue to check each line. 




            // Check if the games has ended and there is no winner : 

            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game Ended

                mGameEnded = true;

                // Iterate every button on the grid
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });


            }
        }
    
    }
}
