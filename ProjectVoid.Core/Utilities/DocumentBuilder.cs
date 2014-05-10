using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.Core.Utilities
{
    public class DocumentBuilder
    {
        #region Fields

        private Brush _ActiveForeground;
        private Brush _ActiveBackground;
        private List<Block> _Blocks = new List<Block>();

        #endregion Fields

        #region Constructors

        public DocumentBuilder(Brush foreground, Brush background)
        {
            _ActiveForeground = foreground;
            _ActiveBackground = background;
        }

        #endregion Constructors

        #region Properties

        public List<Block> Blocks
        {
            get { return _Blocks; }
            set { _Blocks = value; }
        }

        public Brush ActiveForeground
        {
            get { return _ActiveForeground; }
            set { _ActiveForeground = value; }
        }

        public Brush ActiveBackground
        {
            get { return _ActiveBackground; }
            set { _ActiveBackground = value; }
        }

        #endregion Properties

        #region Public Methods

        public void AddParagraph()
        {
            Paragraph paragraph = new Paragraph();

            _Blocks.Add(paragraph);
        }

        public void AddRun(string text)
        {
            if (Blocks.Count <= 0)
            {
                AddParagraph();
            }

            Run run = new Run()
            {
                Text = text,
                Foreground = _ActiveForeground,
                Background = _ActiveBackground,
            };

            ((Paragraph)Blocks[Blocks.Count - 1]).Inlines.Add(run);
        }

        #endregion Public Methods
    }
}