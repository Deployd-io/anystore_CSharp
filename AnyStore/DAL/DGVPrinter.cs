using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Diagnostics;


//[module:CLSCompliant(true)]
namespace DGVPrinterHelper //AllocationRequest
{
    #region Supporting Classes

    /// <summary>
    /// Setup and implements logs for internal logging
    /// </summary>
    class LogManager
    {
        /// <summary>
        /// Path to log file
        /// </summary>
        private String basepath;
        public String BasePath
        {
            get { return basepath; }
            set { basepath = value; }
        }

        /// <summary>
        /// Header for log file name
        /// </summary>
        private String logheader;
        public String LogNameHeader
        {
            get { return logheader; }
            set { logheader = value; }
        }

        private int useFrame = 1;

        /// <summary>
        /// Define logging message categories
        /// </summary>
        public enum Categories
        {
            Info = 1,
            Warning,
            Error,
            Exception
        }

        /// <summary>
        /// Constructor, allow user to override path and name of logging file
        /// </summary>
        /// <param name="userbasepath"></param>
        /// <param name="userlogname"></param>
        public LogManager(String userbasepath, String userlogname)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering LogManager(userbasepath={0},userlogname={1})", userbasepath, userlogname));
            BasePath = String.IsNullOrEmpty(userbasepath) ? "." : userbasepath;
            LogNameHeader = String.IsNullOrEmpty(userlogname) ? "MsgLog" : userlogname;
			System.Diagnostics.Trace.WriteLine(string.Format("LogManager(userbasepath={0},userlogname={1}): LogNameHeader → {2}", userbasepath, userlogname, LogNameHeader));
			System.Diagnostics.Trace.WriteLine(string.Format("LogManager(userbasepath={0},userlogname={1}): LogNameHeader → {2}", userbasepath, userlogname, LogNameHeader));
			System.Diagnostics.Trace.WriteLine(string.Format("LogManager(userbasepath={0},userlogname={1}): BasePath → {2}", userbasepath, userlogname, BasePath));

            Log(Categories.Info, "********************* New Trace *********************");
        }

        /// <summary>
        /// Log a message, using the provided category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="msg"></param>
        public void Log(Categories category, String msg)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Log(category={0},msg={1})", category, msg));
            // get call stack
            StackTrace stackTrace = new StackTrace();
			System.Diagnostics.Trace.WriteLine(string.Format("Log(category={0},msg={1}): stackTrace → {2}", category, msg, stackTrace));

            // get calling method name
            String caller = stackTrace.GetFrame(useFrame).GetMethod().Name;
			System.Diagnostics.Trace.WriteLine(string.Format("Log(category={0},msg={1}): caller → {2}", category, msg, caller));

            // log it
            LogWriter.Write(caller, category, msg, BasePath, LogNameHeader);

            // reset frame pointer
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Log(category={0},msg={1})", category, msg));
            useFrame = 1;
			System.Diagnostics.Trace.WriteLine(string.Format("Log(category={0},msg={1}): useFrame → {2}", category, msg, useFrame));
        }

        /// <summary>
        /// Log an informational message
        /// </summary>
        /// <param name="msg"></param>
        public void LogInfoMsg(String msg)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering LogInfoMsg(msg={0})", msg));
            useFrame++; // bump up the stack frame pointer to skip this entry
            Log(Categories.Info, msg);
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting LogInfoMsg(msg={0})", msg));
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="msg"></param>
        public void LogErrorMsg(String msg)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering LogErrorMsg(msg={0})", msg));
            useFrame++; // bump up the stack frame pointer to skip this entry
            Log(Categories.Error, msg);
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting LogErrorMsg(msg={0})", msg));
        }

        /// <summary>
        /// Log an exception
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Log(ex={0})", ex));
            useFrame++; // bump up the stack frame pointer to skip this entry
            Log(Categories.Exception, String.Format("{0} from {1}", ex.Message, ex.Source));
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Log(ex={0})", ex));
        }
    }

    /// <summary>
    /// Do the actual log writing using setup info in Log Manager class
    /// </summary>
    class LogWriter
    {
        /// <summary>
        /// Create standard log file name with "our" name format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static String LogFileName(String name)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering LogFileName(name={0})", name));
            System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting LogFileName(name={0})", name));
            return String.Format("{0}_{1:yyyyMMdd}.Log", name, DateTime.Now);
        }

        /// <summary>
        /// Write the log entry to the file. Note that the log file is always flushed and closed. This
        /// will impact performance, but ensures that messages aren't lost
        /// </summary>
        /// <param name="from"></param>
        /// <param name="category"></param>
        /// <param name="msg"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void Write(String from, LogManager.Categories category, String msg, String path, String name)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Write(from={0},category={1},msg={2},path={3},name={4})", from, category, msg, path, name));
            StringBuilder line = new StringBuilder();
            line.Append(DateTime.Now.ToShortDateString().ToString());
			System.Diagnostics.Trace.WriteLine(string.Format("Write(from={0},category={1},msg={2},path={3},name={4}): line → {5}", from, category, msg, path, name, line));
            line.Append("-");
            line.Append(DateTime.Now.ToLongTimeString().ToString());
            line.Append(", ");
            line.Append(category.ToString().PadRight(6, ' '));
            line.Append(",");
            line.Append(from.PadRight(13, ' '));
            line.Append(",");
            line.Append(msg);
            StreamWriter w = new StreamWriter(path + "\\" + LogFileName(name), true);
			System.Diagnostics.Trace.WriteLine(string.Format("Write(from={0},category={1},msg={2},path={3},name={4}): w → {5}", from, category, msg, path, name, w));
            w.WriteLine(line.ToString());
            w.Flush();
            w.Close();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Write(from={0},category={1},msg={2},path={3},name={4})", from, category, msg, path, name));
        }
    }

    /// <summary>
    /// Class for the ownerdraw event. Provide the caller with the cell data, the current
    /// graphics context and the location in which to draw the cell.
    /// </summary>
    public class DGVCellDrawingEventArgs : EventArgs
    {
        public Graphics g;
        public RectangleF DrawingBounds;
        public DataGridViewCellStyle CellStyle;
        public int row;
        public int column;
        public Boolean Handled;

        public DGVCellDrawingEventArgs(Graphics g, RectangleF bounds, DataGridViewCellStyle style,
            int row, int column)
            : base()
        {
            this.g = g;
            DrawingBounds = bounds;
            CellStyle = style;
            this.row = row;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVCellDrawingEventArgs(g={0},bounds={1},style={2},row={3},column={4}): CellStyle → {5}", g, bounds, style, row, column, CellStyle));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVCellDrawingEventArgs(g={0},bounds={1},style={2},row={3},column={4}): row → {5}", g, bounds, style, row, column, row));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVCellDrawingEventArgs(g={0},bounds={1},style={2},row={3},column={4}): this.row → {5}", g, bounds, style, row, column, this.row));
        System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting DGVCellDrawingEventArgs(g={0},bounds={1},style={2},row={3},column={4})", g, bounds, style, row, column));
            this.column = column;
            Handled = false;
        }
    }

    /// <summary>
    /// Delegate for ownerdraw cells - allow the caller to provide drawing for the cell
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CellOwnerDrawEventHandler(object sender, DGVCellDrawingEventArgs e);

    /// <summary>
    /// Hold Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extension method to print all the "ImbeddedImages" in a provided list
        /// </summary>
        /// <typeparam name="?"></typeparam>
        /// <param name="list"></param>
        /// <param name="g"></param>
        /// <param name="pagewidth"></param>
        /// <param name="pageheight"></param>
        /// <param name="margins"></param>
        public static void DrawImbeddedImage<T>(this IEnumerable<T> list,
            Graphics g, int pagewidth, int pageheight, Margins margins)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering DrawImbeddedImage(list={0},g={1},pagewidth={2},pageheight={3},margins={4})", list, g, pagewidth, pageheight, margins));
            foreach (T t in list)
            {
                if (t is DGVPrinter.ImbeddedImage)
                {
                    DGVPrinter.ImbeddedImage ii = (DGVPrinter.ImbeddedImage)Convert.ChangeType(t, typeof(DGVPrinter.ImbeddedImage));
					System.Diagnostics.Trace.WriteLine(string.Format("DrawImbeddedImage(list={0},g={1},pagewidth={2},pageheight={3},margins={4}): ii → {5}", list, g, pagewidth, pageheight, margins, ii));
                    // Fix - DrawImageUnscaled was actually scaling the images!!?! Oh well...
                    //g.DrawImageUnscaled(ii.theImage, ii.upperleft(pagewidth, pageheight, margins));
                    g.DrawImage(ii.theImage,
                        new Rectangle(ii.upperleft(pagewidth, pageheight, margins),
                        new Size(ii.theImage.Width, ii.theImage.Height)));
                }
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting DrawImbeddedImage(list={0},g={1},pagewidth={2},pageheight={3},margins={4})", list, g, pagewidth, pageheight, margins));
        }

    }

    #endregion

    /// <summary>
    /// Data Grid View Printer. Print functions for a datagridview, since MS
    /// didn't see fit to do it.
    /// </summary>
    public class DGVPrinter
    {
        public enum Alignment { NotSet, Left, Right, Center }
        public enum Location { Header, Footer, Absolute }
        public enum SizeType { CellSize, StringSize, Porportional }
        public enum PrintLocation { All, FirstOnly, LastOnly, None }

        //---------------------------------------------------------------------
        // internal classes/structs
        //---------------------------------------------------------------------
        #region Internal Classes

        // Identify the reason for a new page when tracking rows
        enum paging { keepgoing, outofroom, datachange };

        // Allow the user to provide images that will be printed as either logos in the
        // header and/or footer or watermarked as in printed behind the text.
        public class ImbeddedImage
        {
            public Image theImage { get; set; }
            public Alignment ImageAlignment { get; set; }
            public Location ImageLocation { get; set; }
            public Int32 ImageX { get; set; }
            public Int32 ImageY { get; set; }

            internal Point upperleft(int pagewidth, int pageheight, Margins margins)
            {
				System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering upperleft(pagewidth={0},pageheight={1},margins={2})", pagewidth, pageheight, margins));
                int y = 0;
                int x = 0;
				System.Diagnostics.Trace.WriteLine(string.Format("upperleft(pagewidth={0},pageheight={1},margins={2}): x → {3}", pagewidth, pageheight, margins, x));
				System.Diagnostics.Trace.WriteLine(string.Format("upperleft(pagewidth={0},pageheight={1},margins={2}): y → {3}", pagewidth, pageheight, margins, y));

                // if we've been given an absolute location, just use it
                if (ImageLocation == Location.Absolute)
                    return new Point(ImageX, ImageY);

                // set the y location based on header or footer
                switch (ImageLocation)
                {
                    case Location.Header:
                        y = margins.Top;
                        break;
                    case Location.Footer:
                        y = pageheight - theImage.Height - margins.Bottom;
                        break;
                    default:
						System.Diagnostics.Trace.WriteLine(string.Format("Throwing ArgumentException from upperleft(pagewidth={0},pageheight={1},margins={2})", pagewidth, pageheight, margins));
                        throw new ArgumentException(String.Format("Unkown value: {0}", ImageLocation));
                }

                // set the x location based on left,right,center
                switch (ImageAlignment)
                {
                    case Alignment.Left:
                        x = margins.Left;
                        break;
                    case Alignment.Center:
                        x = (int)(pagewidth / 2 - theImage.Width / 2) + margins.Left;
                        break;
                    case Alignment.Right:
                        x = (int)(pagewidth - theImage.Width) + margins.Left;
                        break;
                    case Alignment.NotSet:
                        x = ImageX;
                        break;
                    default:
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting upperleft(pagewidth={0},pageheight={1},margins={2})", pagewidth, pageheight, margins));
                        throw new ArgumentException(String.Format("Unkown value: {0}", ImageAlignment));
                }

                return new Point(x, y);
            }
        }

        public IList<ImbeddedImage> ImbeddedImageList = new List<ImbeddedImage>();

        // handle wide-column printing - that is, lists of columns that extend
        // wider than one page width. Columns are broken up into "Page Sets" that
        // are printed one after another until all columns are printed.
        class PageDef
        {
            public PageDef(Margins m, int count, int pagewidth)
            {
				System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PageDef(m={0},count={1},pagewidth={2})", m, count, pagewidth));
                columnindex = new List<int>(count);
                colstoprint = new List<object>(count);
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): colstoprint → {3}", m, count, pagewidth, colstoprint));
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): columnindex → {3}", m, count, pagewidth, columnindex));
                colwidths = new List<float>(count);
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): colwidths → {3}", m, count, pagewidth, colwidths));
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): colwidths → {3}", m, count, pagewidth, colwidths));
                colwidthsoverride = new List<float>(count);
                coltotalwidth = 0;
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): colwidthsoverride → {3}", m, count, pagewidth, colwidthsoverride));
                margins = (Margins)m.Clone();
                pageWidth = pagewidth;
				System.Diagnostics.Trace.WriteLine(string.Format("PageDef(m={0},count={1},pagewidth={2}): margins → {3}", m, count, pagewidth, margins));
            }

            public List<int> columnindex;
            public List<object> colstoprint;
            public List<float> colwidths;
            public List<float> colwidthsoverride;
            public float coltotalwidth;
            public Margins margins;
            private int pageWidth;

            public int printWidth
            {
                get { return pageWidth - margins.Left - margins.Right; }
            }
        }
        IList<PageDef> pagesets;
        int currentpageset = 0;

        // class to hold settings for the PrintDialog presented to the user during
        // the print process
        public class PrintDialogSettingsClass
        {
            public bool AllowSelection = true;
            public bool AllowSomePages = true;
            public bool AllowCurrentPage = true;
            public bool AllowPrintToFile = false;
            public bool ShowHelp = true;
            public bool ShowNetwork = true;
            public bool UseEXDialog = true;
        }

        // class to identify row data for printing
        public class rowdata
        {
            public DataGridViewRow row = null;
            public float height = 0;
            public bool pagebreak = false;
            public bool splitrow = false;
        }
        #endregion

        //---------------------------------------------------------------------
        // global variables
        //---------------------------------------------------------------------
        #region global variables

        // the data grid view we're printing
        DataGridView dgv = null;

        // print document
        PrintDocument printDoc = null;

        // logging 
        LogManager Logger = null;

        // print status items
        Boolean EmbeddedPrinting = false;
        List<rowdata> rowstoprint;
        IList colstoprint;          // divided into pagesets for printing
        int lastrowprinted = -1;
        int currentrow = -1;
        int fromPage = 0;
        int toPage = -1;
        const int maxPages = 2147483647;

        // page formatting options
        int pageHeight = 0;
        float staticheight = 0;
        float rowstartlocation = 0;
        int pageWidth = 0;
        int printWidth = 0;
        float rowheaderwidth = 0;
        int CurrentPage = 0;
        int totalpages;
        PrintRange printRange;

        // calculated values
        //private float headerHeight = 0;
        private float footerHeight = 0;
        private float pagenumberHeight = 0;
        private float colheaderheight = 0;
        //private List<float> rowheights;
        private List<float> colwidths;
        //private List<List<SizeF>> cellsizes;

        #endregion

        //---------------------------------------------------------------------
        // properties - settable by user
        //---------------------------------------------------------------------
        #region properties

        #region global properties

        /// <summary>
        /// Enable logging of of the print process. Default is to log to a file named
        /// 'DGVPrinter_yyyymmdd.Log' in the current directory. Since logging may have 
        /// an impact on performance, it should be used for troubleshooting purposes only.
        /// </summary>
        protected Boolean enablelogging;
        public Boolean EnableLogging
        {
            get { return enablelogging; }
            set
            {
                enablelogging = value;
                if (enablelogging)
                {
                    Logger = new LogManager(".", "DGVPrinter");
                }
            }
        }

        /// <summary>
        /// Allow the user to change the logging directory. Setting this enables logging by default.
        /// </summary>
        public String LogDirectory
        {
            get
            {
                if (null != Logger)
                    return Logger.BasePath;
                else
                    return null;
            }
            set
            {
                if (null == Logger)
                    EnableLogging = true;
                Logger.BasePath = value;
            }
        }

        /// <summary>
        /// OwnerDraw Event declaration. Callers can subscribe to this event to override the 
        /// cell drawing.
        /// </summary>
        public event CellOwnerDrawEventHandler OwnerDraw;

        /// <summary>
        /// provide an override for the print preview dialog "owner" field
        /// Note: Changed style for VS2005 compatibility
        /// </summary>
        //public Form Owner
        //{ get; set; }
        protected Form _Owner = null;
        public Form Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        /// <summary>
        /// provide an override for the print preview zoom setting
        /// Note: Changed style for VS2005 compatibility
        /// </summary>
        //public Double PrintPreviewZoom
        //{ get; set; }
        protected Double _PrintPreviewZoom = 1.0;
        public Double PrintPreviewZoom
        {
            get { return _PrintPreviewZoom; }
            set { _PrintPreviewZoom = value; }
        }


        /// <summary>
        /// expose printer settings to allow access to calling program
        /// </summary>
        public PrinterSettings PrintSettings
        {
            get { return printDoc.PrinterSettings; }
        }

        /// <summary>
        /// expose settings for the PrintDialog displayed to the user
        /// </summary>
        private PrintDialogSettingsClass printDialogSettings = new PrintDialogSettingsClass();
        public PrintDialogSettingsClass PrintDialogSettings
        {
            get { return printDialogSettings; }
        }

        /// <summary>
        /// Set Printer Name
        /// </summary>
        private String printerName;
        public String PrinterName
        {
            get { return printerName; }
            set { printerName = value; }
        }

        /// <summary>
        /// Allow access to the underlying print document
        /// </summary>
        public PrintDocument printDocument
        {
            get { return printDoc; }
            set { printDoc = value; }
        }

        /// <summary>
        /// Allow caller to set the upper-left corner icon used
        /// in the print preview dialog
        /// </summary>
        private Icon ppvIcon = null;
        public Icon PreviewDialogIcon
        {
            get { return ppvIcon; }
            set { ppvIcon = value; }
        }

        /// <summary>
        /// Allow caller to set print preview dialog
        /// </summary>
        private PrintPreviewDialog previewdialog = null;
        public PrintPreviewDialog PreviewDialog
        {
            get { return previewdialog; }
            set { previewdialog = value; }
        }

        /// <summary>
        /// Flag to control whether or not we print the Page Header
        /// </summary>
        private Boolean printHeader = true;
        public Boolean PrintHeader
        {
            get { return printHeader; }
            set { printHeader = value; }
        }

        /// <summary>
        /// Determine the height of the header
        /// </summary>
        private float HeaderHeight
        {
            get
            {
                float headerheight = 0;

                // Add in title and subtitle heights - this is sensitive to 
                // wether or not titles are printed on the current page
                // TitleHeight and SubTitleHeight have their respective spacing
                // already included
                headerheight += TitleHeight + SubTitleHeight;

                // Add in column header heights
                if ((bool)PrintColumnHeaders)
                {
                    headerheight += colheaderheight;
                }

                // return calculated height
                return headerheight;
            }
        }

        /// <summary>
        /// Flag to control whether or not we print the Page Footer
        /// </summary>
        private Boolean printFooter = true;
        public Boolean PrintFooter
        {
            get { return printFooter; }
            set { printFooter = value; }
        }

        /// <summary>
        /// Flag to control whether or not we print the Column Header line
        /// </summary>
        private Boolean? printColumnHeaders;
        public Boolean? PrintColumnHeaders
        {
            get { return printColumnHeaders; }
            set { printColumnHeaders = value; }
        }

        /// <summary>
        /// Flag to control whether or not we print the Column Header line
        /// Defaults to False to match previous functionality
        /// </summary>
        private Boolean? printRowHeaders = false;
        public Boolean? PrintRowHeaders
        {
            get { return printRowHeaders; }
            set { printRowHeaders = value; }
        }

        /// <summary>
        /// Flag to control whether rows are printed whole or if partial
        /// rows should be printed to fill the bottom of the page. Turn this
        /// "Off" (i.e. false) to print cells/rows deeper than one page
        /// </summary>
        private Boolean keepRowsTogether = true;
        public Boolean KeepRowsTogether
        {
            get { return keepRowsTogether; }
            set { keepRowsTogether = value; }
        }

        /// <summary>
        /// How much of a row must show on the current page before it is 
        /// split when KeepRowsTogether is set to true.
        /// </summary>
        private float keeprowstogethertolerance = 15;
        public float KeepRowsTogetherTolerance
        {
            get { return keeprowstogethertolerance; }
            set { keeprowstogethertolerance = value; }
        }

        #endregion

        // Title
        #region title properties

        // override flag
        bool overridetitleformat = false;

        // formatted height of title
        float titleheight = 0;

        /// <summary>
        /// Title for this report. Default is empty.
        /// </summary>
        private String title;
        public String Title
        {
            get { return title; }
            set
            {
                title = value;
                if (docName == null)
                {
                    printDoc.DocumentName = value;
                }
            }
        }

        /// <summary>
        /// Name of the document. Default is report title (can be empty)
        /// </summary>
        private String docName;
        public String DocName
        {
            get { return docName; }
            set { printDoc.DocumentName = value; docName = value; }
        }

        /// <summary>
        /// Font for the title. Default is Tahoma, 18pt.
        /// </summary>
        private Font titlefont;
        public Font TitleFont
        {
            get { return titlefont; }
            set { titlefont = value; }
        }

        /// <summary>
        /// Foreground color for the title. Default is Black
        /// </summary>
        private Color titlecolor;
        public Color TitleColor
        {
            get { return titlecolor; }
            set { titlecolor = value; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private StringFormat titleformat;
        public StringFormat TitleFormat
        {
            get { return titleformat; }
            set { titleformat = value; overridetitleformat = true; }
        }

        /// <summary>
        /// Allow the user to override the title string alignment. Default value is 
        /// Alignment - Near; 
        /// </summary>
        public StringAlignment TitleAlignment
        {
            get { return titleformat.Alignment; }
            set
            {
                titleformat.Alignment = value;
                overridetitleformat = true;
            }
        }

        /// <summary>
        /// Allow the user to override the title string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        public StringFormatFlags TitleFormatFlags
        {
            get { return titleformat.FormatFlags; }
            set
            {
                titleformat.FormatFlags = value;
                overridetitleformat = true;
            }
        }

        /// <summary>
        /// Control where in the document the title prints
        /// </summary>
        private PrintLocation titleprint = PrintLocation.All;
        public PrintLocation TitlePrint
        {
            get { return titleprint; }
            set { titleprint = value; }
        }

        /// <summary>
        /// Return the title height based whether to print it or not
        /// </summary>
        private float TitleHeight
        {
            get
            {
                if (PrintLocation.All == TitlePrint)
                    return titleheight + titlespacing;

                if ((PrintLocation.FirstOnly == TitlePrint) && (1 == CurrentPage))
                    return titleheight + titlespacing;

                if ((PrintLocation.LastOnly == TitlePrint) && (totalpages == CurrentPage))
                    return titleheight + titlespacing;

                return 0;
            }
        }

        /// <summary>
        /// Mandatory spacing between the grid and the footer
        /// </summary>
        private float titlespacing;
        public float TitleSpacing
        {
            get { return titlespacing; }
            set { titlespacing = value; }
        }

        /// <summary>
        /// Title Block Background Color
        /// </summary>
        private Brush titlebackground;
        public Brush TitleBackground
        {
            get { return titlebackground; }
            set { titlebackground = value; }
        }

        /// <summary>
        /// Title Block Border
        /// </summary>
        private Pen titleborder;
        public Pen TitleBorder
        {
            get { return titleborder; }
            set { titleborder = value; }
        }

        #endregion

        // SubTitle
        #region subtitle properties

        // override flat
        bool overridesubtitleformat = false;

        // formatted height of subtitle
        float subtitleheight = 0;

        /// <summary>
        /// SubTitle for this report. Default is empty.
        /// </summary>
        private String subtitle;
        public String SubTitle
        {
            get { return subtitle; }
            set { subtitle = value; }
        }

        /// <summary>
        /// Font for the subtitle. Default is Tahoma, 12pt.
        /// </summary>
        private Font subtitlefont;
        public Font SubTitleFont
        {
            get { return subtitlefont; }
            set { subtitlefont = value; }
        }

        /// <summary>
        /// Foreground color for the subtitle. Default is Black
        /// </summary>
        private Color subtitlecolor;
        public Color SubTitleColor
        {
            get { return subtitlecolor; }
            set { subtitlecolor = value; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private StringFormat subtitleformat;
        public StringFormat SubTitleFormat
        {
            get { return subtitleformat; }
            set { subtitleformat = value; overridesubtitleformat = true; }
        }

        /// <summary>
        /// Allow the user to override the subtitle string alignment. Default value is 
        /// Alignment - Near; 
        /// </summary>
        public StringAlignment SubTitleAlignment
        {
            get { return subtitleformat.Alignment; }
            set
            {
                subtitleformat.Alignment = value;
                overridesubtitleformat = true;
            }
        }

        /// <summary>
        /// Allow the user to override the subtitle string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        public StringFormatFlags SubTitleFormatFlags
        {
            get { return subtitleformat.FormatFlags; }
            set
            {
                subtitleformat.FormatFlags = value;
                overridesubtitleformat = true;
            }
        }

        /// <summary>
        /// Control where in the document the title prints
        /// </summary>
        private PrintLocation subtitleprint = PrintLocation.All;
        public PrintLocation SubTitlePrint
        {
            get { return subtitleprint; }
            set { subtitleprint = value; }
        }

        /// <summary>
        /// Return the title height based whether to print it or not
        /// </summary>
        private float SubTitleHeight
        {
            get
            {
                if (PrintLocation.All == SubTitlePrint)
                    return subtitleheight + subtitlespacing;

                if ((PrintLocation.FirstOnly == SubTitlePrint) && (1 == CurrentPage))
                    return subtitleheight + subtitlespacing;

                if ((PrintLocation.LastOnly == SubTitlePrint) && (totalpages == CurrentPage))
                    return subtitleheight + subtitlespacing;

                return 0;
            }
        }

        /// <summary>
        /// Mandatory spacing between the grid and the footer
        /// </summary>
        private float subtitlespacing;
        public float SubTitleSpacing
        {
            get { return subtitlespacing; }
            set { subtitlespacing = value; }
        }

        /// <summary>
        /// Title Block Background Color
        /// </summary>
        private Brush subtitlebackground;
        public Brush SubTitleBackground
        {
            get { return subtitlebackground; }
            set { subtitlebackground = value; }
        }

        /// <summary>
        /// Title Block Border
        /// </summary>
        private Pen subtitleborder;
        public Pen SubTitleBorder
        {
            get { return subtitleborder; }
            set { subtitleborder = value; }
        }

        #endregion

        // Footer
        #region footer properties

        // override flag
        bool overridefooterformat = false;

        /// <summary>
        /// footer for this report. Default is empty.
        /// </summary>
        private String footer;
        public String Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        /// <summary>
        /// Font for the footer. Default is Tahoma, 10pt.
        /// </summary>
        private Font footerfont;
        public Font FooterFont
        {
            get { return footerfont; }
            set { footerfont = value; }
        }

        /// <summary>
        /// Foreground color for the footer. Default is Black
        /// </summary>
        private Color footercolor;
        public Color FooterColor
        {
            get { return footercolor; }
            set { footercolor = value; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private StringFormat footerformat;
        public StringFormat FooterFormat
        {
            get { return footerformat; }
            set { footerformat = value; overridefooterformat = true; }
        }

        /// <summary>
        /// Allow the user to override the footer string alignment. Default value is 
        /// Alignment - Center; 
        /// </summary>
        public StringAlignment FooterAlignment
        {
            get { return footerformat.Alignment; }
            set
            {
                footerformat.Alignment = value;
                overridefooterformat = true;
            }
        }

        /// <summary>
        /// Allow the user to override the footer string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        public StringFormatFlags FooterFormatFlags
        {
            get { return footerformat.FormatFlags; }
            set
            {
                footerformat.FormatFlags = value;
                overridefooterformat = true;
            }
        }

        /// <summary>
        /// Mandatory spacing between the grid and the footer
        /// </summary>
        private float footerspacing;
        public float FooterSpacing
        {
            get { return footerspacing; }
            set { footerspacing = value; }
        }

        /// <summary>
        /// Control where in the document the title prints
        /// </summary>
        private PrintLocation footerprint = PrintLocation.All;
        public PrintLocation FooterPrint
        {
            get { return footerprint; }
            set { footerprint = value; }
        }

        /// <summary>
        /// Determine the height of the footer
        /// </summary>
        private float FooterHeight
        {
            get
            {
                float footerheight = 0;

                // return calculated height if we're printing the footer
                if ((PrintLocation.All == FooterPrint)
                    || ((PrintLocation.FirstOnly == FooterPrint) && (1 == CurrentPage))
                    || ((PrintLocation.LastOnly == FooterPrint) && (totalpages == CurrentPage)))
                {
                    // Add in footer text height 
                    footerheight += footerHeight + FooterSpacing;
                }

                return footerheight;
            }
        }

        /// <summary>
        /// Title Block Background Color
        /// </summary>
        private Brush footerbackground;
        public Brush FooterBackground
        {
            get { return footerbackground; }
            set { footerbackground = value; }
        }

        /// <summary>
        /// Title Block Border
        /// </summary>
        private Pen footerborder;
        public Pen FooterBorder
        {
            get { return footerborder; }
            set { footerborder = value; }
        }

        #endregion

        // Page Numbering
        #region page number properties

        // override flag
        bool overridepagenumberformat = false;

        /// <summary>
        /// Include page number in the printout. Default is true.
        /// </summary>
        private bool pageno = true;
        public bool PageNumbers
        {
            get { return pageno; }
            set { pageno = value; }
        }

        /// <summary>
        /// Font for the page number, Default is Tahoma, 8pt.
        /// </summary>
        private Font pagenofont;
        public Font PageNumberFont
        {
            get { return pagenofont; }
            set { pagenofont = value; }
        }

        /// <summary>
        /// Text color (foreground) for the page number. Default is Black
        /// </summary>
        private Color pagenocolor;
        public Color PageNumberColor
        {
            get { return pagenocolor; }
            set { pagenocolor = value; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private StringFormat pagenumberformat;
        public StringFormat PageNumberFormat
        {
            get { return pagenumberformat; }
            set { pagenumberformat = value; overridepagenumberformat = true; }
        }

        /// <summary>
        /// Allow the user to override the page number string alignment. Default value is 
        /// Alignment - Near; 
        /// </summary>
        public StringAlignment PageNumberAlignment
        {
            get { return pagenumberformat.Alignment; }
            set
            {
                pagenumberformat.Alignment = value;
                overridepagenumberformat = true;
            }
        }

        /// <summary>
        /// Allow the user to override the pagenumber string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        public StringFormatFlags PageNumberFormatFlags
        {
            get { return pagenumberformat.FormatFlags; }
            set
            {
                pagenumberformat.FormatFlags = value;
                overridepagenumberformat = true;
            }
        }

        /// <summary>
        /// Allow the user to select whether to have the page number at the top or bottom
        /// of the page. Default is false: page numbers on the bottom of the page
        /// </summary>
        private bool pagenumberontop = false;
        public bool PageNumberInHeader
        {
            get { return pagenumberontop; }
            set { pagenumberontop = value; }
        }

        /// <summary>
        /// Should the page number be printed on a separate line, or printed on the
        /// same line as the header / footer? Default is false;
        /// </summary>
        private bool pagenumberonseparateline = false;
        public bool PageNumberOnSeparateLine
        {
            get { return pagenumberonseparateline; }
            set { pagenumberonseparateline = value; }
        }

        /// <summary>
        /// Show the total page number as n of total 
        /// </summary>
        private bool showtotalpagenumber = false;
        public bool ShowTotalPageNumber
        {
            get { return showtotalpagenumber; }
            set { showtotalpagenumber = value; }
        }

        /// <summary>
        /// Text separating page number and total page number. Default is ' of '.
        /// </summary>
        private String pageseparator = " of ";
        public String PageSeparator
        {
            get { return pageseparator; }
            set { pageseparator = value; }
        }

        private String pagetext = "Page ";
        public String PageText
        {
            get { return pagetext; }
            set { pagetext = value; }
        }

        private String parttext = " - Part ";
        public String PartText
        {
            get { return parttext; }
            set { parttext = value; }
        }

        /// <summary>
        /// Control where in the document the title prints
        /// </summary>
        private PrintLocation pagenumberprint = PrintLocation.All;
        public PrintLocation PageNumberPrint
        {
            get { return pagenumberprint; }
            set { pagenumberprint = value; }
        }

        /// <summary>
        /// Determine the height of the footer
        /// </summary>
        private float PageNumberHeight
        {
            get
            {
                // return calculated height if we're printing the footer
                if ((PrintLocation.All == PageNumberPrint)
                    || ((PrintLocation.FirstOnly == PageNumberPrint) && (1 == CurrentPage))
                    || ((PrintLocation.LastOnly == PageNumberPrint) && (totalpages == CurrentPage)))
                {
                    // return page number height if we're printing it on a separate line
                    // if we're not printing on a separate line, but we're suppressing the
                    // header or footer then we still need to reserve space for the page number
                    if (pagenumberonseparateline)
                        return pagenumberHeight;
                    else if (pagenumberontop && 0 == TitleHeight && 0 == SubTitleHeight)
                    {
                        return pagenumberHeight;
                    }
                    else if (!pagenumberontop && 0 == FooterHeight)
                    {
                        return footerspacing + pagenumberHeight;
                    }
                }

                return 0;
            }
        }

        #endregion

        // Header Cell Printing 
        #region header cell properties

        private DataGridViewCellStyle rowheaderstyle;
        public DataGridViewCellStyle RowHeaderCellStyle
        {
            get { return rowheaderstyle; }
            set { rowheaderstyle = value; }
        }

        /// <summary>
        /// Allow override of the row header cell format object
        /// </summary>
        private StringFormat rowheadercellformat = null;
        public StringFormat GetRowHeaderCellFormat(DataGridView grid)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetRowHeaderCellFormat(grid={0})", grid));
            // get default values from provided data grid view, but only
            // if we don't already have a header cell format
            if ((null != grid) && (null == rowheadercellformat))
            {
                buildstringformat(ref rowheadercellformat, grid.Rows[0].HeaderCell.InheritedStyle,
                    headercellalignment, StringAlignment.Near, headercellformatflags,
                    StringTrimming.Word);
            }

            // if we still don't have a header cell format, create an empty
            if (null == rowheadercellformat)
                rowheadercellformat = new StringFormat(headercellformatflags);
				System.Diagnostics.Trace.WriteLine(string.Format("GetRowHeaderCellFormat(grid={0}): rowheadercellformat → {1}", grid, rowheadercellformat));
				System.Diagnostics.Trace.WriteLine(string.Format("GetRowHeaderCellFormat(grid={0}): rowheadercellformat → {1}", grid, rowheadercellformat));

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetRowHeaderCellFormat(grid={0})", grid));
            return rowheadercellformat;
        }

        /// <summary>
        /// Default value to show in the row header cell if no value is provided in the DataGridView.
        /// Defaults to one tab space
        /// </summary>
        private String rowheadercelldefaulttext = "\t";
        public String RowHeaderCellDefaultText
        {
            get { return rowheadercelldefaulttext; }
            set { rowheadercelldefaulttext = value; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private Dictionary<string, DataGridViewCellStyle> columnheaderstyles =
            new Dictionary<string, DataGridViewCellStyle>();
        public Dictionary<string, DataGridViewCellStyle> ColumnHeaderStyles
        {
            get { return columnheaderstyles; }
        }

        /// <summary>
        /// Allow override of the header cell format object
        /// </summary>
        private StringFormat columnheadercellformat = null;
        public StringFormat GetColumnHeaderCellFormat(DataGridView grid)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetColumnHeaderCellFormat(grid={0})", grid));
            // get default values from provided data grid view, but only
            // if we don't already have a header cell format
            if ((null != grid) && (null == columnheadercellformat))
            {
                buildstringformat(ref columnheadercellformat, grid.Columns[0].HeaderCell.InheritedStyle,
                    headercellalignment, StringAlignment.Near, headercellformatflags,
                    StringTrimming.Word);
            }

            // if we still don't have a header cell format, create an empty
            if (null == columnheadercellformat)
                columnheadercellformat = new StringFormat(headercellformatflags);
				System.Diagnostics.Trace.WriteLine(string.Format("GetColumnHeaderCellFormat(grid={0}): columnheadercellformat → {1}", grid, columnheadercellformat));
				System.Diagnostics.Trace.WriteLine(string.Format("GetColumnHeaderCellFormat(grid={0}): columnheadercellformat → {1}", grid, columnheadercellformat));

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetColumnHeaderCellFormat(grid={0})", grid));
            return columnheadercellformat;
        }

        /// <summary>
        /// Deprecated - use HeaderCellFormat
        /// Allow the user to override the header cell string alignment. Default value is 
        /// Alignment - Near; 
        /// </summary>
        private StringAlignment headercellalignment;
        public StringAlignment HeaderCellAlignment
        {
            get { return headercellalignment; }
            set { headercellalignment = value; }
        }

        /// <summary>
        /// Deprecated - use HeaderCellFormat
        /// Allow the user to override the header cell string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        private StringFormatFlags headercellformatflags;
        public StringFormatFlags HeaderCellFormatFlags
        {
            get { return headercellformatflags; }
            set { headercellformatflags = value; }
        }
        #endregion

        // Individual Cell Printing
        #region cell properties

        /// <summary>
        /// Allow override of the cell printing format
        /// </summary>
        private StringFormat cellformat = null;
        public StringFormat GetCellFormat(DataGridView grid)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetCellFormat(grid={0})", grid));
            // get default values from provided data grid view, but only
            // if we don't already have a cell format
            if ((null != grid) && (null == cellformat))
            {
                buildstringformat(ref cellformat, grid.Rows[0].Cells[0].InheritedStyle,
                    cellalignment, StringAlignment.Near, cellformatflags,
                    StringTrimming.Word);
            }

            // if we still don't have a cell format, create an empty
            if (null == cellformat)
                cellformat = new StringFormat(cellformatflags);
				System.Diagnostics.Trace.WriteLine(string.Format("GetCellFormat(grid={0}): cellformat → {1}", grid, cellformat));
				System.Diagnostics.Trace.WriteLine(string.Format("GetCellFormat(grid={0}): cellformat → {1}", grid, cellformat));

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetCellFormat(grid={0})", grid));
            return cellformat;
        }

        /// <summary>
        /// Deprecated - use GetCellFormat
        /// Allow the user to override the cell string alignment. Default value is 
        /// Alignment - Near; 
        /// </summary>
        private StringAlignment cellalignment;
        public StringAlignment CellAlignment
        {
            get { return cellalignment; }
            set { cellalignment = value; }
        }

        /// <summary>
        /// Deprecated - use GetCellFormat
        /// Allow the user to override the cell string format flags. Default values
        /// are: FormatFlags - NoWrap, LineLimit, NoClip
        /// </summary>
        private StringFormatFlags cellformatflags;
        public StringFormatFlags CellFormatFlags
        {
            get { return cellformatflags; }
            set { cellformatflags = value; }
        }

        /// <summary>
        /// allow the user to override the column width calcs with their own defaults
        /// </summary>
        private List<float> colwidthsoverride = new List<float>();
        private Dictionary<string, float> publicwidthoverrides = new Dictionary<string, float>();
        public Dictionary<string, float> ColumnWidths
        {
            get { return publicwidthoverrides; }
        }

        /// <summary>
        /// Allow per column style overrides
        /// </summary>
        private Dictionary<string, DataGridViewCellStyle> colstyles =
            new Dictionary<string, DataGridViewCellStyle>();
        public Dictionary<string, DataGridViewCellStyle> ColumnStyles
        {
            get { return colstyles; }
        }

        /// <summary>
        /// Allow per column style overrides
        /// </summary>
        private Dictionary<string, DataGridViewCellStyle> altrowcolstyles =
            new Dictionary<string, DataGridViewCellStyle>();
        public Dictionary<string, DataGridViewCellStyle> AlternatingRowColumnStyles
        {
            get { return altrowcolstyles; }
        }

        /// <summary>
        /// Allow the user to set columns that appear on every pageset. Only used when 
        /// the printout is wider than one page.
        /// </summary>
        private List<int> fixedcolumns = new List<int>();
        private List<string> fixedcolumnnames = new List<string>();
        public List<string> FixedColumns
        {
            get { return fixedcolumnnames; }
        }

        /// <summary>
        /// List of columns to not display in the grid view printout.
        /// </summary>
        private List<String> hidecolumns = new List<string>();
        public List<String> HideColumns
        {
            get { return hidecolumns; }
        }

        /// <summary>
        /// Insert a page break when the value in this column changes
        /// </summary>
        private object oldvalue = null;
        private String breakonvaluechange;
        public String BreakOnValueChange
        {
            get { return breakonvaluechange; }
            set { breakonvaluechange = value; }
        }

        #endregion

        // Page Level Properties
        #region page level properties

        /// <summary>
        /// Page margins override. Default is (60, 60, 40, 40)
        /// </summary>
        public Margins PrintMargins
        {
            get { return PageSettings.Margins; }
            set { PageSettings.Margins = value; }
        }

        /// <summary>
        /// Expose the printdocument default page settings to the caller
        /// </summary>
        public PageSettings PageSettings
        {
            get { return printDoc.DefaultPageSettings; }
        }

        /// <summary>
        /// Spread the columns porportionally accross the page. Default is false.
        /// Deprecated. Please use the ColumnWidth property
        /// </summary>
        private bool porportionalcolumns = false;
        public bool PorportionalColumns
        {
            get { return porportionalcolumns; }
            set
            {
                porportionalcolumns = value;
                if (porportionalcolumns)
                    ColumnWidth = ColumnWidthSetting.Porportional;
                else
                    ColumnWidth = ColumnWidthSetting.CellWidth;
            }
        }

        /// <summary>
        /// Center the table on the page. 
        /// </summary>
        private Alignment tablealignment = Alignment.NotSet;
        public Alignment TableAlignment
        {
            get { return tablealignment; }
            set { tablealignment = value; }
        }

        /// <summary>
        /// Change the default row height to either the height of the string or the size of 
        /// the cell. Added for image cell handling; set to CellHeight for image cells
        /// </summary>
        public enum RowHeightSetting { DataHeight, CellHeight }
        private RowHeightSetting _rowheight = RowHeightSetting.DataHeight;
        public RowHeightSetting RowHeight
        {
            get { return _rowheight; }
            set { _rowheight = value; }
        }

        /// <summary>
        /// Change the default column width to be spread porportionally accross the page,
        /// to the size of the grid cell or the size of the formatted data string.
        /// Set to CellWidth for image cells.
        /// </summary>
        public enum ColumnWidthSetting { DataWidth, CellWidth, Porportional }
        private ColumnWidthSetting _rowwidth = ColumnWidthSetting.CellWidth;
        public ColumnWidthSetting ColumnWidth
        {
            get { return _rowwidth; }
            set
            {
                _rowwidth = value;
                if (value == ColumnWidthSetting.Porportional)
                    porportionalcolumns = true;
                else
                    porportionalcolumns = false;
            }
        }

        #endregion

        // Utility Functions
        #region
        /// <summary>
        /// calculate the print preview window width to show the entire page
        /// </summary>
        /// <returns></returns>
        private int PreviewDisplayWidth()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering PreviewDisplayWidth()");
            double displayWidth = printDoc.DefaultPageSettings.Bounds.Width
                + 3 * printDoc.DefaultPageSettings.HardMarginY;
			System.Diagnostics.Trace.WriteLine("<<< Exiting PreviewDisplayWidth()");
            return (int)(displayWidth * PrintPreviewZoom);
        }

        /// <summary>
        /// calculate the print preview window height to show the entire page
        /// </summary>
        /// <returns></returns>
        private int PreviewDisplayHeight()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering PreviewDisplayHeight()");
            double displayHeight = printDoc.DefaultPageSettings.Bounds.Height
                + 3 * printDoc.DefaultPageSettings.HardMarginX;

			System.Diagnostics.Trace.WriteLine("<<< Exiting PreviewDisplayHeight()");
            return (int)(displayHeight * PrintPreviewZoom);
        }

        /// <summary>
        /// Invoke any provided cell owner draw routines
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCellOwnerDraw(DGVCellDrawingEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering OnCellOwnerDraw(e={0})", e));
            if (null != OwnerDraw)
                OwnerDraw(this, e);
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting OnCellOwnerDraw(e={0})", e));
        }

        /// <summary>
        /// Given a row and column, get the current grid cell style, including our local 
        /// overrides
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        protected DataGridViewCellStyle GetStyle(DataGridViewRow row, DataGridViewColumn col)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetStyle(row={0},col={1})", row, col));
            // set initial default
            DataGridViewCellStyle colstyle = row.Cells[col.Index].InheritedStyle.Clone();
			System.Diagnostics.Trace.WriteLine(string.Format("GetStyle(row={0},col={1}): colstyle → {2}", row, col, colstyle));

            // check for our override
            if (ColumnStyles.ContainsKey(col.Name))
            {
                colstyle = ColumnStyles[col.Name];
            }

            // check for alternating row override
            if (0 != (row.Index & 1) && AlternatingRowColumnStyles.ContainsKey(col.Name))
            {
                colstyle = AlternatingRowColumnStyles[col.Name];
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetStyle(row={0},col={1})", row, col));
            return colstyle;
        }

        /// <summary>
        /// Skim the colstoprint list for a column name and return it's index
        /// </summary>
        /// <param name="colname">Name of column to find</param>
        /// <returns>index of column</returns>
        protected int GetColumnIndex(string colname)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetColumnIndex(colname={0})", colname));
            int i = 0;
            foreach (DataGridViewColumn col in colstoprint)
            {
                if (col.Name != colname)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            // catch unknown column names
            if (i >= colstoprint.Count)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Throwing Exception from GetColumnIndex(colname={0})", colname));
                throw new Exception("Unknown Column Name: " + colname);
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetColumnIndex(colname={0})", colname));
            return i;
        }

        #endregion

        #endregion

        //---------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------
        /// <summary>
        /// Constructor for DGVPrinter
        /// </summary>
        public DGVPrinter()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering DGVPrinter()");
            // create print document
            printDoc = new PrintDocument();
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): printDoc → {0}", printDoc));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): printDoc → {0}", printDoc));
            //printDoc.PrintPage += new PrintPageEventHandler(PrintPageEventHandler);
            //printDoc.BeginPrint += new PrintEventHandler(BeginPrintEventHandler);
            PrintMargins = new Margins(60, 60, 40, 40);
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): PrintMargins → {0}", PrintMargins));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): PrintMargins → {0}", PrintMargins));

            // set default fonts
            pagenofont = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Point);
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): pagenofont → {0}", pagenofont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): pagenofont → {0}", pagenofont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): titlefont → {0}", titlefont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): subtitlefont → {0}", subtitlefont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): titlefont → {0}", titlefont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): footerfont → {0}", footerfont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): titlecolor → {0}", titlecolor));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): subtitlefont → {0}", subtitlefont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): subtitlecolor → {0}", subtitlecolor));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): footerfont → {0}", footerfont));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): footercolor → {0}", footercolor));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): titlespacing → {0}", titlespacing));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): subtitlespacing → {0}", subtitlespacing));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): footerspacing → {0}", footerspacing));
            pagenocolor = Color.Black;
            titlefont = new Font("Tahoma", 18, FontStyle.Bold, GraphicsUnit.Point);
            titlecolor = Color.Black;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): pagenocolor → {0}", pagenocolor));
            subtitlefont = new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Point);
            subtitlecolor = Color.Black;
            footerfont = new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Point);
            footercolor = Color.Black;

            // default spacing
            titlespacing = 0;
            subtitlespacing = 0;
            footerspacing = 0;

            // Create string formatting objects
            buildstringformat(ref titleformat, null, StringAlignment.Center, StringAlignment.Center,
                StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
            buildstringformat(ref subtitleformat, null, StringAlignment.Center, StringAlignment.Center,
                StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
            buildstringformat(ref footerformat, null, StringAlignment.Center, StringAlignment.Center,
                StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
            buildstringformat(ref pagenumberformat, null, StringAlignment.Far, StringAlignment.Center,
                StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);

            // Set these formatting objects to null to flag whether or not they were set by the caller
            columnheadercellformat = null;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): columnheadercellformat → {0}", columnheadercellformat));
            rowheadercellformat = null;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): rowheadercellformat → {0}", rowheadercellformat));
            cellformat = null;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): cellformat → {0}", cellformat));

            // Print Preview properties
            Owner = null;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): Owner → {0}", Owner));
            PrintPreviewZoom = 1.0;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): PrintPreviewZoom → {0}", PrintPreviewZoom));

            // Deprecated properties - retain for backwards compatibility
			System.Diagnostics.Trace.WriteLine("<<< Exiting DGVPrinter()");
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): headercellformatflags → {0}", headercellformatflags));
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): cellformatflags → {0}", cellformatflags));
            headercellalignment = StringAlignment.Near;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): headercellalignment → {0}", headercellalignment));
            headercellformatflags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            cellalignment = StringAlignment.Near;
			System.Diagnostics.Trace.WriteLine(string.Format("DGVPrinter(): cellalignment → {0}", cellalignment));
            cellformatflags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        }


        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        // Primary Interface - Presents a dialog and then prints or previews the 
        // indicated data grid view
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        /// <summary>
        /// Start the printing process, print to a printer.
        /// </summary>
        /// <param name="dgv">The DataGridView to print</param>
        /// NOTE: Any changes to this method also need to be done in PrintPreviewDataGridView
        public void PrintDataGridView(DataGridView dgv)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintDataGridView(dgv={0})", dgv));
            if (EnableLogging) Logger.LogInfoMsg("PrintDataGridView process started");
            if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintDataGridView(dgv={0})", dgv));
            if (!(typeof(DataGridView).IsInstanceOfType(dgv))) {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }

            // save the datagridview we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintDataGridView(dgv={0}): dgv → {1}", dgv, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("PrintDataGridView(dgv={0}): this.dgv → {1}", dgv, this.dgv));

            // display dialog and print
            if (DialogResult.OK == DisplayPrintDialog())
            {
                PrintNoDisplay(dgv);
            }
        }

        /// <summary>
        /// Start the printing process, print to a print preview dialog
        /// </summary>
        /// <param name="dgv">The DataGridView to print</param>
        /// NOTE: Any changes to this method also need to be done in PrintDataGridView
        public void PrintPreviewDataGridView(DataGridView dgv)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintPreviewDataGridView(dgv={0})", dgv));
            if (EnableLogging) Logger.LogInfoMsg("PrintPreviewDataGridView process started");
            if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintPreviewDataGridView(dgv={0})", dgv));
            if (!(typeof(DataGridView).IsInstanceOfType(dgv))) {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }

            // save the datagridview we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewDataGridView(dgv={0}): dgv → {1}", dgv, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewDataGridView(dgv={0}): this.dgv → {1}", dgv, this.dgv));

            // display dialog and print
            if (DialogResult.OK == DisplayPrintDialog())
            {
                PrintPreviewNoDisplay(dgv);
            }
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        // Alternative Interface. In order to set the print information correctly
        // either the DisplayPrintDialog() routine must be called, OR the 
        // PrintDocument (and PrinterSettings) must be Handled through calling
        // PrintDialog separately.
        //
        // Once the PrintDocument has been setup, the PrintNoDisplay() and/or
        // PrintPreviewNoDisplay() routines can be called to print multiple
        // DataGridViews using the same print setup.
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        /// <summary>
        /// Display a printdialog and return the result. Either this method or 
        /// the equivalent must be done prior to calling either of the PrintNoDisplay
        /// or PrintPreviewNoDisplay methods.
        /// </summary>
        /// <returns></returns>
        public DialogResult DisplayPrintDialog()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering DisplayPrintDialog()");
            if (EnableLogging) Logger.LogInfoMsg("DisplayPrintDialog process started");
            // create new print dialog and set options
            PrintDialog pd = new PrintDialog();
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd → {0}", pd));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.AllowSelection → {0}", pd.AllowSelection));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.AllowSomePages → {0}", pd.AllowSomePages));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.AllowCurrentPage → {0}", pd.AllowCurrentPage));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.AllowPrintToFile → {0}", pd.AllowPrintToFile));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.ShowHelp → {0}", pd.ShowHelp));
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.ShowNetwork → {0}", pd.ShowNetwork));
            pd.UseEXDialog = printDialogSettings.UseEXDialog;
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.UseEXDialog → {0}", pd.UseEXDialog));
            pd.AllowSelection = printDialogSettings.AllowSelection;
            pd.AllowSomePages = printDialogSettings.AllowSomePages;
            pd.AllowCurrentPage = printDialogSettings.AllowCurrentPage;
            pd.AllowPrintToFile = printDialogSettings.AllowPrintToFile;
            pd.ShowHelp = printDialogSettings.ShowHelp;
            pd.ShowNetwork = printDialogSettings.ShowNetwork;

            //// setup print dialog with internal setttings
            pd.Document = printDoc;
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayPrintDialog(): pd.Document → {0}", pd.Document));
            if (!String.IsNullOrEmpty(printerName))
                printDoc.PrinterSettings.PrinterName = printerName;

            // show the dialog and display the result
			System.Diagnostics.Trace.WriteLine("<<< Exiting DisplayPrintDialog()");
            return pd.ShowDialog();
        }

        /// <summary>
        /// Print the provided grid view. Either DisplayPrintDialog() or it's equivalent
        /// setup must be completed prior to calling this routine
        /// </summary>
        /// <param name="dgv"></param>
        public void PrintNoDisplay(DataGridView dgv)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintNoDisplay(dgv={0})", dgv));
            if (EnableLogging) Logger.LogInfoMsg("PrintNoDisplay process started");
            if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintNoDisplay(dgv={0})", dgv));
            if (!(dgv is DataGridView)) {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }

            // save the grid we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintNoDisplay(dgv={0}): dgv → {1}", dgv, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("PrintNoDisplay(dgv={0}): this.dgv → {1}", dgv, this.dgv));

            printDoc.PrintPage += new PrintPageEventHandler(PrintPageEventHandler);
            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEventHandler);

            // setup and do printing
            SetupPrint();
            printDoc.Print();
        }

        /// <summary>
        /// Preview the provided grid view. Either DisplayPrintDialog() or it's equivalent
        /// setup must be completed prior to calling this routine
        /// </summary>
        /// <param name="dgv"></param>
        public void PrintPreviewNoDisplay(DataGridView dgv)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintPreviewNoDisplay(dgv={0})", dgv));
            if (EnableLogging) Logger.LogInfoMsg("PrintPreviewNoDisplay process started");
            if (null == dgv) throw new Exception("Null Parameter passed to DGVPrinter.");
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintPreviewNoDisplay(dgv={0})", dgv));
            if (!(dgv is DataGridView)) {
                throw new Exception("Invalid Parameter passed to DGVPrinter.");
            }

            // save the grid we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): dgv → {1}", dgv, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): this.dgv → {1}", dgv, this.dgv));

            printDoc.PrintPage += new PrintPageEventHandler(PrintPageEventHandler);
            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEventHandler);

            // display the preview dialog
            SetupPrint();

            // if the caller hasn't provided a print preview dialog, then create one
            if (null == PreviewDialog)
                PreviewDialog = new PrintPreviewDialog();
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog → {1}", dgv, PreviewDialog));
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog → {1}", dgv, PreviewDialog));

            // set up dialog for preview
            PreviewDialog.Document = printDoc;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog.Document → {1}", dgv, PreviewDialog.Document));
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog.Owner → {1}", dgv, PreviewDialog.Owner));
            PreviewDialog.UseAntiAlias = true;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog.UseAntiAlias → {1}", dgv, PreviewDialog.UseAntiAlias));
            PreviewDialog.Owner = Owner;
            PreviewDialog.PrintPreviewControl.Zoom = PrintPreviewZoom;
            PreviewDialog.Width = PreviewDisplayWidth();
            PreviewDialog.Height = PreviewDisplayHeight();

            if (null != ppvIcon)
                PreviewDialog.Icon = ppvIcon;
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPreviewNoDisplay(dgv={0}): PreviewDialog.Icon → {1}", dgv, PreviewDialog.Icon));

            // show the dialog
            PreviewDialog.ShowDialog();
        }


        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        // Print Process Interface Methods
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        // NOTE: This is retained only for backward compatibility, and should 
        // not be used for printing grid views that might be larger than the 
        // input print area.
        public Boolean EmbeddedPrint(DataGridView dgv, Graphics g, Rectangle area)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering EmbeddedPrint(dgv={0},g={1},area={2})", dgv, g, area));
            if (EnableLogging) Logger.LogInfoMsg("EmbeddedPrint process started");
            // verify we've been set up properly
            if ((null == dgv))
                throw new Exception("Null Parameter passed to DGVPrinter.");

            // set the embedded print flag
            EmbeddedPrinting = true;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): EmbeddedPrinting → {3}", dgv, g, area, EmbeddedPrinting));

            // save the grid we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): dgv → {3}", dgv, g, area, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): this.dgv → {3}", dgv, g, area, this.dgv));

            //-----------------------------------------------------------------
            // Force setting for embedded printing
            //-----------------------------------------------------------------

            // set margins so we print within the provided area
            Margins saveMargins = PrintMargins;
            PrintMargins.Top = area.Top;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintMargins.Top → {3}", dgv, g, area, PrintMargins.Top));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintMargins.Left → {3}", dgv, g, area, PrintMargins.Left));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintMargins.Right → {3}", dgv, g, area, PrintMargins.Right));
            PrintMargins.Bottom = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintMargins.Bottom → {3}", dgv, g, area, PrintMargins.Bottom));
            PrintMargins.Left = area.Left;
            PrintMargins.Right = 0;

            // set "page" height and width to our destination area
            pageHeight = area.Height + area.Top;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): pageHeight → {3}", dgv, g, area, pageHeight));
            printWidth = area.Width;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): printWidth → {3}", dgv, g, area, printWidth));
            pageWidth = area.Width + area.Left;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): pageWidth → {3}", dgv, g, area, pageWidth));

            // force 'off' header and footer
            PrintHeader = false;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintHeader → {3}", dgv, g, area, PrintHeader));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): pageno → {3}", dgv, g, area, pageno));
            PrintFooter = false;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrint(dgv={0},g={1},area={2}): PrintFooter → {3}", dgv, g, area, PrintFooter));
            pageno = false;

            //-----------------------------------------------------------------
            // Determine what's going to be printed and set the columns to print
            //-----------------------------------------------------------------
            SetupPrint();

            //-----------------------------------------------------------------
            // Do a single "Print" and return false - we're just printing what
            // we can in the space provided.
            //-----------------------------------------------------------------
            PrintPage(g);
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting EmbeddedPrint(dgv={0},g={1},area={2})", dgv, g, area));
            return false;
        }

        public void EmbeddedPrintMultipageSetup(DataGridView dgv, Rectangle area)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering EmbeddedPrintMultipageSetup(dgv={0},area={1})", dgv, area));
            if (EnableLogging) Logger.LogInfoMsg("EmbeddedPrintMultipageSetup process started");
            // verify we've been set up properly
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting EmbeddedPrintMultipageSetup(dgv={0},area={1})", dgv, area));
            if ((null == dgv)) {
                throw new Exception("Null Parameter passed to DGVPrinter.");
            }

            // set the embedded print flag
            EmbeddedPrinting = true;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): EmbeddedPrinting → {2}", dgv, area, EmbeddedPrinting));

            // save the grid we're printing
            this.dgv = dgv;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): dgv → {2}", dgv, area, dgv));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): this.dgv → {2}", dgv, area, this.dgv));

            //-----------------------------------------------------------------
            // Force setting for embedded printing
            //-----------------------------------------------------------------

            // set margins so we print within the provided area
            Margins saveMargins = PrintMargins;
            PrintMargins.Top = area.Top;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintMargins.Top → {2}", dgv, area, PrintMargins.Top));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintMargins.Left → {2}", dgv, area, PrintMargins.Left));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintMargins.Right → {2}", dgv, area, PrintMargins.Right));
            PrintMargins.Bottom = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintMargins.Bottom → {2}", dgv, area, PrintMargins.Bottom));
            PrintMargins.Left = area.Left;
            PrintMargins.Right = 0;

            // set "page" height and width to our destination area
            pageHeight = area.Height + area.Top;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): pageHeight → {2}", dgv, area, pageHeight));
            printWidth = area.Width;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): printWidth → {2}", dgv, area, printWidth));
            pageWidth = area.Width + area.Left;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): pageWidth → {2}", dgv, area, pageWidth));

            // force 'off' header and footer
            PrintHeader = false;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintHeader → {2}", dgv, area, PrintHeader));
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): pageno → {2}", dgv, area, pageno));
            PrintFooter = false;
			System.Diagnostics.Trace.WriteLine(string.Format("EmbeddedPrintMultipageSetup(dgv={0},area={1}): PrintFooter → {2}", dgv, area, PrintFooter));
            pageno = false;

            //-----------------------------------------------------------------
            // Determine what's going to be printed and set the columns to print
            //-----------------------------------------------------------------
            SetupPrint();
        }

        /// <summary>
        /// BeginPrint Event Handler
        /// Set values at start of print run
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BeginPrintEventHandler(object sender, PrintEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering BeginPrintEventHandler(sender={0},e={1})", sender, e));
            if (EnableLogging) Logger.LogInfoMsg("BeginPrintEventHandler called. Printing started.");
            // reset counters since we'll go through this twice if we print from preview
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting BeginPrintEventHandler(sender={0},e={1})", sender, e));
            currentpageset = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("BeginPrintEventHandler(sender={0},e={1}): currentpageset → {2}", sender, e, currentpageset));
            lastrowprinted = -1;
			System.Diagnostics.Trace.WriteLine(string.Format("BeginPrintEventHandler(sender={0},e={1}): lastrowprinted → {2}", sender, e, lastrowprinted));
            CurrentPage = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("BeginPrintEventHandler(sender={0},e={1}): CurrentPage → {2}", sender, e, CurrentPage));
        }

        /// <summary>
        /// PrintPage event handler. This routine prints one page. It will
        /// skip non-printable pages if the user selected the "some pages" option
        /// on the print dialog.
        /// </summary>
        /// <param name="sender">default object from windows</param>
        /// <param name="e">Event info from Windows about the printing</param>
        public void PrintPageEventHandler(object sender, PrintPageEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintPageEventHandler(sender={0},e={1})", sender, e));
            if (EnableLogging) Logger.LogInfoMsg("PrintPageEventHandler called. Printing a page.");
            e.HasMorePages = PrintPage(e.Graphics);
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintPageEventHandler(sender={0},e={1})", sender, e));
        }


        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        // Internal Methods
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        /// <summary>
        /// Set up the print job. Save information from print dialog
        /// and print document for easy access. Also sets up the rows
        /// and columns that will be printed. At this point, we're 
        /// collecting all columns in colstoprint. This will be broken
        /// up into pagesets later on 
        /// </summary>
        void SetupPrint()
        {
            if (EnableLogging)
            {
                Logger.LogInfoMsg("SetupPrint process started");
                var m = printDoc.DefaultPageSettings.Margins;
                Logger.LogInfoMsg(String.Format("Initial Printer Margins are {0}, {1}, {2}, {3}", m.Left, m.Right, m.Top, m.Bottom));
            }

            if (null == PrintColumnHeaders)
                PrintColumnHeaders = dgv.ColumnHeadersVisible;

            if (null == PrintRowHeaders)
                PrintRowHeaders = dgv.RowHeadersVisible;

            // Set the default row header style where we don't have an override
            // and we do have rows
            if ((null == RowHeaderCellStyle) && (0 != dgv.Rows.Count))
                RowHeaderCellStyle = dgv.Rows[0].HeaderCell.InheritedStyle;

            /* Functionality to come - redo of styling
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                // Set the default column styles where we've not been given an override
                if (!ColumnStyles.ContainsKey(col.Name))
                    ColumnStyles[col.Name] = dgv.Columns[col.Name].InheritedStyle;
                // Set the default column header styles where we don't have an override
                if (!ColumnHeaderStyles.ContainsKey(col.Name))
                    ColumnHeaderStyles[col.Name] = dgv.Columns[col.Name].HeaderCell.InheritedStyle;
            }
            */

            //-----------------------------------------------------------------
            // Set row and column headercell and normal cell print formats if they were not
            // explicitly set by the caller
            //-----------------------------------------------------------------
            if (null == columnheadercellformat)
                buildstringformat(ref columnheadercellformat, dgv.Columns[0].HeaderCell.InheritedStyle,
                    headercellalignment, StringAlignment.Near, headercellformatflags,
                    StringTrimming.Word);
            if (null == rowheadercellformat)
                buildstringformat(ref rowheadercellformat, RowHeaderCellStyle,
                    headercellalignment, StringAlignment.Near, headercellformatflags,
                    StringTrimming.Word);
            if (null == cellformat)
                buildstringformat(ref cellformat, dgv.DefaultCellStyle,
                    cellalignment, StringAlignment.Near, cellformatflags,
                    StringTrimming.Word);

            //-----------------------------------------------------------------
            // get info on the limits of the printer's actual print area available. Convert
            // to int's to work with margins.
            //
            // note: do this only if we're not doing embedded printing.
            //-----------------------------------------------------------------

            if (!EmbeddedPrinting)
            {
                int printareawidth;
                int hardx = (int)Math.Round(printDoc.DefaultPageSettings.HardMarginX);
                int hardy = (int)Math.Round(printDoc.DefaultPageSettings.HardMarginY);
                if (printDoc.DefaultPageSettings.Landscape)
                    printareawidth = (int)Math.Round(printDoc.DefaultPageSettings.PrintableArea.Height);
                else
                    printareawidth = (int)Math.Round(printDoc.DefaultPageSettings.PrintableArea.Width);

                //-----------------------------------------------------------------
                // set the print area we're working within
                //-----------------------------------------------------------------

                pageHeight = printDoc.DefaultPageSettings.Bounds.Height;
                pageWidth = printDoc.DefaultPageSettings.Bounds.Width;

                //-----------------------------------------------------------------
                // Set the printable area: margins and pagewidth
                //-----------------------------------------------------------------

                // Set initial printer margins 
                PrintMargins = printDoc.DefaultPageSettings.Margins;

                // adjust for when the margins are less than the printer's hard x/y limits
                PrintMargins.Right = (hardx > PrintMargins.Right) ? hardx : PrintMargins.Right;
                PrintMargins.Left = (hardx > PrintMargins.Left) ? hardx : PrintMargins.Left;
                PrintMargins.Top = (hardy > PrintMargins.Top) ? hardy : PrintMargins.Top;
                PrintMargins.Bottom = (hardy > PrintMargins.Bottom) ? hardy : PrintMargins.Bottom;

                // Now, we can calc default print width, again, respecting the printer's limitations
                printWidth = pageWidth - PrintMargins.Left - PrintMargins.Right;
                printWidth = (printWidth > printareawidth) ? printareawidth : printWidth;

                // log margin changes
                if (EnableLogging)
                {
                    Logger.LogInfoMsg(String.Format("Printer 'Hard' X limit is {0} and 'Hard' Y limit is {1}", hardx, hardy));
                    Logger.LogInfoMsg(String.Format("Printer height limit is {0} and width limit is {1}, print width is {2}",
                        pageHeight, pageWidth, printWidth));
                    Logger.LogInfoMsg(String.Format("Final overall margins are {0}, {1}, {2}, {3}",
                        PrintMargins.Left, PrintMargins.Right, PrintMargins.Top, PrintMargins.Bottom));
                    Logger.LogInfoMsg(String.Format("Table Alignment is {0}", TableAlignment.ToString()));
                }
            }

            //-----------------------------------------------------------------
            // Figure out which pages / rows to print
            //-----------------------------------------------------------------

            // save print range 
            printRange = printDoc.PrinterSettings.PrintRange;
            if (EnableLogging) Logger.LogInfoMsg(String.Format("PrintRange is {0}", printRange));

            // pages to print handles "some pages" option
            if (PrintRange.SomePages == printRange)
            {
                // set limits to only print some pages
                fromPage = printDoc.PrinterSettings.FromPage;
                toPage = printDoc.PrinterSettings.ToPage;
            }
            else
            {
                // set extremes so that we'll print all pages
                fromPage = 0;
                toPage = maxPages;
            }

            //-----------------------------------------------------------------
            // Determine what's going to be printed
            //-----------------------------------------------------------------
            SetupPrintRange();

            //-----------------------------------------------------------------
            // Set up width overrides and fixed columns
            //-----------------------------------------------------------------
            SetupColumns();

            //-----------------------------------------------------------------
            // Now that we know what we're printing, measure the print area and
            // count the pages.
            //-----------------------------------------------------------------

            // Measure the print area
            measureprintarea(printDoc.PrinterSettings.CreateMeasurementGraphics());

            // Count the pages
            totalpages = Pagination();

        }

        /// <summary>
        /// Set up width override and fixed columns lists
        /// </summary>
        private void SetupColumns()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering SetupColumns()");
            // identify fixed columns by their column number in the print list
            foreach (string colname in fixedcolumnnames)
            {
                try
                {
                    fixedcolumns.Add(GetColumnIndex(colname));
                }
                catch// (Exception ex)
                {
                    // missing column, so add it to print list and retry
                    colstoprint.Add(dgv.Columns[colname]);
                    fixedcolumns.Add(GetColumnIndex(colname));
                }
            }

            // Adjust override list to have the same number of entries as colstoprint,
            foreach (DataGridViewColumn col in colstoprint)
                if (publicwidthoverrides.ContainsKey(col.Name))
                    colwidthsoverride.Add(publicwidthoverrides[col.Name]);
                else
                    colwidthsoverride.Add(-1);

        }

        /// <summary>
        /// Determine the print range based on dialog selections and user input. The rows
        /// and columns are sorted to ensure that the rows appear in their correct index 
        /// order and the columns appear in DisplayIndex order to account for added columns
        /// and re-ordered columns.
        /// </summary>
        private void SetupPrintRange()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering SetupPrintRange()");
            //-----------------------------------------------------------------
            // set up the rows and columns to print
            //
            // Note: The "Selectedxxxx" lists in the datagridview are 'stacks' that
            //  have the selected items pushed in the *in the order they were selected*
            //  i.e. not the order you want to print them in!
            //-----------------------------------------------------------------
            SortedList temprowstoprint = null;
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): temprowstoprint → {0}", temprowstoprint));
            SortedList tempcolstoprint = null;
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): tempcolstoprint → {0}", tempcolstoprint));

            // rows to print (handles "selection" and "current page" options
            if (PrintRange.Selection == printRange)
            {
                temprowstoprint = new SortedList(dgv.SelectedCells.Count);
                tempcolstoprint = new SortedList(dgv.SelectedCells.Count);

                //if DGV has rows selected, it's easy, selected rows and all visible columns
                if (0 != dgv.SelectedRows.Count)
                {
                    temprowstoprint = new SortedList(dgv.SelectedRows.Count);
                    tempcolstoprint = new SortedList(dgv.Columns.Count);

                    // sort the rows into index order
                    temprowstoprint = new SortedList(dgv.SelectedRows.Count);
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                        if (row.Visible && !row.IsNewRow)
                            temprowstoprint.Add(row.Index, row);

                    // sort the columns into display order
                    foreach (DataGridViewColumn col in dgv.Columns) if (col.Visible) tempcolstoprint.Add(col.DisplayIndex, col);
                }
                // if selected columns, then all rows, and selected columns
                else if (0 != dgv.SelectedColumns.Count)
                {
                    temprowstoprint = new SortedList(dgv.Rows.Count);
                    tempcolstoprint = new SortedList(dgv.SelectedColumns.Count);

                    foreach (DataGridViewRow row in dgv.Rows)
                        if (row.Visible && !row.IsNewRow)
                            temprowstoprint.Add(row.Index, row);

                    foreach (DataGridViewColumn col in dgv.SelectedColumns)
                        if (col.Visible)
                            tempcolstoprint.Add(col.DisplayIndex, col);
                }
                // we just have a bunch of selected cells so we have to do some work
                else
                {
                    // set up sorted lists. the selectedcells method does not guarantee
                    // that the cells will always be in left-right top-bottom order. 
                    temprowstoprint = new SortedList(dgv.SelectedCells.Count);
                    tempcolstoprint = new SortedList(dgv.SelectedCells.Count);

                    // for each selected cell, add unique rows and columns
                    int displayindex, colindex, rowindex;
                    foreach (DataGridViewCell cell in dgv.SelectedCells)
                    {
                        displayindex = cell.OwningColumn.DisplayIndex;
						System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): displayindex → {0}", displayindex));
                        colindex = cell.ColumnIndex;
						System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): colindex → {0}", colindex));
                        rowindex = cell.RowIndex;
						System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): rowindex → {0}", rowindex));

                        // add unique rows
                        if (!temprowstoprint.Contains(rowindex))
                        {
                            DataGridViewRow row = dgv.Rows[rowindex];
                            if (row.Visible && !row.IsNewRow)
                                temprowstoprint.Add(rowindex, dgv.Rows[rowindex]);
                        }
                        // add unique columns
                        if (!tempcolstoprint.Contains(displayindex))
                            tempcolstoprint.Add(displayindex, dgv.Columns[colindex]);
                    }
                }
            }
            // if current page was selected, print visible columns for the
            // displayed rows                
            else if (PrintRange.CurrentPage == printRange)
            {
                // create lists
                temprowstoprint = new SortedList(dgv.DisplayedRowCount(true));
                tempcolstoprint = new SortedList(dgv.Columns.Count);

                // select all visible rows on displayed page
                for (int i = dgv.FirstDisplayedScrollingRowIndex;
                    i < dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(true);
                    i++)
                {
                    DataGridViewRow row = dgv.Rows[i];
                    if (row.Visible) temprowstoprint.Add(row.Index, row);
                }

                // select all visible columns
                foreach (DataGridViewColumn col in dgv.Columns) if (col.Visible) tempcolstoprint.Add(col.DisplayIndex, col);
            }
            // this is the default for print all - everything marked visible will be printed
            // this is also used when printing specific pages or page ranges as we won't know
            // what to print until we size all the rows
            else
            {
                temprowstoprint = new SortedList(dgv.Rows.Count);
                tempcolstoprint = new SortedList(dgv.Columns.Count);

                // select all visible rows and all visible columns - but don't include the new 'data entry row' 
                foreach (DataGridViewRow row in dgv.Rows) if (row.Visible && !row.IsNewRow) temprowstoprint.Add(row.Index, row);

                // sort the columns into display order
                foreach (DataGridViewColumn col in dgv.Columns) if (col.Visible) tempcolstoprint.Add(col.DisplayIndex, col);
            }

            // move rows and columns into global containers
            rowstoprint = new List<rowdata>(temprowstoprint.Count);
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): rowstoprint → {0}", rowstoprint));
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): rowstoprint → {0}", rowstoprint));
            foreach (object item in temprowstoprint.Values) rowstoprint.Add(new rowdata() { row = (DataGridViewRow)item });

            colstoprint = new List<DataGridViewColumn>(tempcolstoprint.Count);
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): colstoprint → {0}", colstoprint));
			System.Diagnostics.Trace.WriteLine(string.Format("SetupPrintRange(): colstoprint → {0}", colstoprint));
            foreach (object item in tempcolstoprint.Values) colstoprint.Add(item);

            // remove "hidden" columns from list of columns to print
            foreach (String columnname in HideColumns)
            {
                colstoprint.Remove(dgv.Columns[columnname]);
            }

            if (EnableLogging) Logger.LogInfoMsg(String.Format("Grid Printout Range is {0} columns", colstoprint.Count));
			System.Diagnostics.Trace.WriteLine("<<< Exiting SetupPrintRange()");
            if (EnableLogging) Logger.LogInfoMsg(String.Format("Grid Printout Range is {0} rows", rowstoprint.Count));

        }

        /// <summary>
        /// Centralize the string format settings. Build a string format object
        /// using passed in settings, (allowing a user override of a single setting)
        /// and get the alignment from the cell control style.
        /// </summary>
        /// <param name="format">String format, ref parameter with return settings</param>
        /// <param name="controlstyle">DataGridView style to apply (if available)</param>
        /// <param name="alignment">Override text Alignment</param>
        /// <param name="linealignment">Override line alignment</param>
        /// <param name="flags">String format flags</param>
        /// <param name="trim">Override string trimming flags</param>
        /// <returns></returns>
        private void buildstringformat(ref StringFormat format, DataGridViewCellStyle controlstyle,
            StringAlignment alignment, StringAlignment linealignment, StringFormatFlags flags,
            StringTrimming trim)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5})", format, controlstyle, alignment, linealignment, flags, trim));
            // allocate format if it doesn't already exist
            if (null == format)
                format = new StringFormat();
				System.Diagnostics.Trace.WriteLine(string.Format("buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5}): format → {6}", format, controlstyle, alignment, linealignment, flags, trim, format));

            // Set defaults
            format.Alignment = alignment;
			System.Diagnostics.Trace.WriteLine(string.Format("buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5}): format.Alignment → {6}", format, controlstyle, alignment, linealignment, flags, trim, format.Alignment));
			System.Diagnostics.Trace.WriteLine(string.Format("buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5}): format.FormatFlags → {6}", format, controlstyle, alignment, linealignment, flags, trim, format.FormatFlags));
			System.Diagnostics.Trace.WriteLine(string.Format("buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5}): format.Trimming → {6}", format, controlstyle, alignment, linealignment, flags, trim, format.Trimming));
            format.LineAlignment = linealignment;
			System.Diagnostics.Trace.WriteLine(string.Format("buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5}): format.LineAlignment → {6}", format, controlstyle, alignment, linealignment, flags, trim, format.LineAlignment));
            format.FormatFlags = flags;
            format.Trimming = trim;

            // Check on right-to-left flag. This is set at the grid level, but doesn't show up 
            // as a cell format. Urgh.
            if ((null != dgv) && (RightToLeft.Yes == dgv.RightToLeft))
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

            // use cell alignment to override defaulted alignments
            if (null != controlstyle)
            {
                // Adjust the format based on the control settings, bias towards centered
                DataGridViewContentAlignment cellalign = controlstyle.Alignment;
                if (cellalign.ToString().Contains("Center")) format.Alignment = StringAlignment.Center;
                else if (cellalign.ToString().Contains("Left")) format.Alignment = StringAlignment.Near;
                else if (cellalign.ToString().Contains("Right")) format.Alignment = StringAlignment.Far;

                if (cellalign.ToString().Contains("Top")) format.LineAlignment = StringAlignment.Near;
                else if (cellalign.ToString().Contains("Middle")) format.LineAlignment = StringAlignment.Center;
                else if (cellalign.ToString().Contains("Bottom")) format.LineAlignment = StringAlignment.Far;
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting buildstringformat(format={0},controlstyle={1},alignment={2},linealignment={3},flags={4},trim={5})", format, controlstyle, alignment, linealignment, flags, trim));
        }

        /// <summary>
        /// Calculate cell size based on data versus size settings
        /// </summary>
        /// <param name="g">Current graphics context</param>
        /// <param name="cell">Cell being measured</param>
        /// <param name="index">Column index of cell being measured</param>
        /// <param name="cellstyle">Computed Style of cell being measured</param>
        /// <param name="basewidth">Initial width for size calculation</param>
        /// <param name="format">Computed string format for cell data</param>
        /// <returns>Size of printed cell</returns>
        private SizeF calccellsize(Graphics g, DataGridViewCell cell, DataGridViewCellStyle cellstyle,
            float basewidth, float overridewidth, StringFormat format)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5})", g, cell, cellstyle, basewidth, overridewidth, format));
            // Start with the grid view cell size
            SizeF size = new SizeF(cell.Size);
			System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): size → {6}", g, cell, cellstyle, basewidth, overridewidth, format, size));

            // If we need to do any calculated cell sizes, we need to measure the cell contents
            if ((RowHeightSetting.DataHeight == RowHeight) ||
                (ColumnWidthSetting.DataWidth == ColumnWidth) ||
                (ColumnWidthSetting.Porportional == ColumnWidth))
            {
                SizeF datasize;

                //-------------------------------------------------------------
                // Measure cell contents
                //-------------------------------------------------------------
                if (("DataGridViewImageCell" == dgv.Columns[cell.ColumnIndex].CellType.Name)
                    && ("Image" == cell.ValueType.Name || "Byte[]" == cell.ValueType.Name))
                {
                    // image to measure
                    Image img;

                    // if we don't actually have a value, then just exit with a minimum size.
                    if ((null == cell.Value) || (typeof(DBNull) == cell.Value.GetType()))
                        return new SizeF(1, 1);

                    // Check on type of image cell value - may not be an actual "image" type
                    if ("Image" == cell.ValueType.Name || "Object" == cell.ValueType.Name)
                    {
                        // if it's an "image" type, then load it directly
                        img = (System.Drawing.Image)cell.Value;
						System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): img → {6}", g, cell, cellstyle, basewidth, overridewidth, format, img));
                    }
                    else if ("Byte[]" == cell.ValueType.Name)
                    {
						System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): img → {6}", g, cell, cellstyle, basewidth, overridewidth, format, img));
                        // if it's not an "image" type (i.e. loaded from a database to a bound column)
                        // convert the underlying byte array to an image
                        ImageConverter ic = new ImageConverter();
						System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): ic → {6}", g, cell, cellstyle, basewidth, overridewidth, format, ic));
                        img = (Image)ic.ConvertFrom((byte[])cell.Value);
                    }
                    else {
							System.Diagnostics.Trace.WriteLine(string.Format("Throwing Exception from calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5})", g, cell, cellstyle, basewidth, overridewidth, format));
                        throw new Exception(String.Format("Unknown image cell underlying type: {0} in column {1}",
                            cell.ValueType.Name, cell.ColumnIndex));
                    }

                    // size to print is size of image
                    datasize = img.Size;
					System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): datasize → {6}", g, cell, cellstyle, basewidth, overridewidth, format, datasize));
                }
                else
                {
					System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): datasize → {6}", g, cell, cellstyle, basewidth, overridewidth, format, datasize));
                    float width = (-1 != overridewidth) ? overridewidth : basewidth;
					System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): width → {6}", g, cell, cellstyle, basewidth, overridewidth, format, width));

                    // measure the data for each column, keep widths and biggest height
                    datasize = g.MeasureString(cell.EditedFormattedValue.ToString(), cellstyle.Font,
                        new SizeF(width, maxPages), format);

                    // if we have excessively large cell, limit it to one page width
                    if (printWidth < datasize.Width)
                        datasize = g.MeasureString(cell.FormattedValue.ToString(), cellstyle.Font,
                        new SizeF(pageWidth - cellstyle.Padding.Left - cellstyle.Padding.Right, maxPages),
                        format);
                }

                //-------------------------------------------------------------
                // Add in padding for data based cell sizes and porportional columns
                //-------------------------------------------------------------

                // set cell height to string height if indicated
                if (RowHeightSetting.DataHeight == RowHeight)
                    size.Height = datasize.Height + cellstyle.Padding.Top + cellstyle.Padding.Bottom;
					System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): size.Height → {6}", g, cell, cellstyle, basewidth, overridewidth, format, size.Height));

                // set cell width to calculated width if indicated
                if ((ColumnWidthSetting.DataWidth == ColumnWidth) ||
                    (ColumnWidthSetting.Porportional == ColumnWidth))
                    size.Width = datasize.Width + cellstyle.Padding.Left + cellstyle.Padding.Right;
					System.Diagnostics.Trace.WriteLine(string.Format("calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5}): size.Width → {6}", g, cell, cellstyle, basewidth, overridewidth, format, size.Width));
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting calccellsize(g={0},cell={1},cellstyle={2},basewidth={3},overridewidth={4},format={5})", g, cell, cellstyle, basewidth, overridewidth, format));
            return size;
        }

        /// <summary>
        /// Recalculate row heights for cells whose width is greater than the set column width. 
        /// Called when column widths are changed in order to flow text down the page instead of 
        /// accross.
        /// </summary>
        /// <param name="g">Graphics Context for measuring image columns</param>
        /// <param name="colindex">column index in colstoprint</param>
        /// <param name="newcolwidth">new column width</param>
        private void RecalcRowHeights(Graphics g, int colindex, float newcolwidth)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering RecalcRowHeights(g={0},colindex={1},newcolwidth={2})", g, colindex, newcolwidth));
            DataGridViewCell cell = null;
            float finalsize = 0F;
			System.Diagnostics.Trace.WriteLine(string.Format("RecalcRowHeights(g={0},colindex={1},newcolwidth={2}): finalsize → {3}", g, colindex, newcolwidth, finalsize));
			System.Diagnostics.Trace.WriteLine(string.Format("RecalcRowHeights(g={0},colindex={1},newcolwidth={2}): cell → {3}", g, colindex, newcolwidth, cell));

            // search calculated cell sizes for widths larger than our new width
            for (int i = 0; i < rowstoprint.Count; i++)
            {
                cell = ((DataGridViewRow)rowstoprint[i].row).Cells[((DataGridViewColumn)colstoprint[colindex]).Index];

                if (RowHeightSetting.DataHeight == RowHeight)
                {
                    StringFormat currentformat = null;

                    // get column style
                    DataGridViewCellStyle colstyle = GetStyle(((DataGridViewRow)rowstoprint[i].row), ((DataGridViewColumn)colstoprint[colindex]));
					System.Diagnostics.Trace.WriteLine(string.Format("RecalcRowHeights(g={0},colindex={1},newcolwidth={2}): colstyle → {3}", g, colindex, newcolwidth, colstyle));

                    // build the cell style and font 
                    buildstringformat(ref currentformat, colstyle, cellformat.Alignment, cellformat.LineAlignment,
                        cellformat.FormatFlags, cellformat.Trimming);

                    // recalculate cell size using new width. This will flow data down the page and 
                    // change the row height
                    SizeF size = calccellsize(g, cell, colstyle, newcolwidth, colwidthsoverride[colindex], currentformat);
					System.Diagnostics.Trace.WriteLine(string.Format("RecalcRowHeights(g={0},colindex={1},newcolwidth={2}): size → {3}", g, colindex, newcolwidth, size));

                    finalsize = size.Height;
                }
                else
                {
                    finalsize = cell.Size.Height;
                }

                // change the saved row height based on the recalculated size
                rowstoprint[i].height = (rowstoprint[i].height < finalsize ? finalsize : rowstoprint[i].height);
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting RecalcRowHeights(g={0},colindex={1},newcolwidth={2})", g, colindex, newcolwidth));
        }


        /// <summary>
        /// Scan all the rows and columns to be printed and calculate the 
        /// overall individual column width (based on largest column value), 
        /// the header sizes, and determine all the row heights.
        /// </summary>
        /// <param name="g">The graphics context for all measurements</param>
        private void measureprintarea(Graphics g)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering measureprintarea(g={0})", g));
            int i, j;
            colwidths = new List<float>(colstoprint.Count);
			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): colwidths → {1}", g, colwidths));
			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): colwidths → {1}", g, colwidths));
            footerHeight = 0;

			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): footerHeight → {1}", g, footerHeight));
            // temp variables
            DataGridViewColumn col;
            DataGridViewRow row;

            //-----------------------------------------------------------------
            // measure the page headers and footers, including the grid column header cells
            //-----------------------------------------------------------------

            // set initial column sizes based on column titles
            for (i = 0; i < colstoprint.Count; i++)
            {
                col = (DataGridViewColumn)colstoprint[i];
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): col → {1}", g, col));
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): col → {1}", g, col));

                //-------------------------------------------------------------
                // Build String format and Cell style
                //-------------------------------------------------------------

                // get gridview style, and override if we have a set style for this column
                StringFormat currentformat = null;
                DataGridViewCellStyle headercolstyle = col.HeaderCell.InheritedStyle.Clone();
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): headercolstyle → {1}", g, headercolstyle));
                if (ColumnHeaderStyles.ContainsKey(col.Name))
                {
                    headercolstyle = columnheaderstyles[col.Name];

                    // build the cell style and font 
                    buildstringformat(ref currentformat, headercolstyle, cellformat.Alignment, cellformat.LineAlignment,
                        cellformat.FormatFlags, cellformat.Trimming);
                }
                else if (col.HasDefaultCellStyle)
                {
                    // build the cell style and font 
                    buildstringformat(ref currentformat, headercolstyle, cellformat.Alignment, cellformat.LineAlignment,
                        cellformat.FormatFlags, cellformat.Trimming);
                }
                else
                {
                    currentformat = columnheadercellformat;
                }

                //-------------------------------------------------------------
                // Calculate and accumulate column header width and height
                //-------------------------------------------------------------
                SizeF size = col.HeaderCell.Size;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): size → {1}", g, size));

                // deal with overridden col widths
                float usewidth = 0;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): usewidth → {1}", g, usewidth));
                if (0 <= colwidthsoverride[i])
                    //usewidth = colwidthsoverride[i];
                    colwidths.Add(colwidthsoverride[i]);            // override means set that size
                else if ((ColumnWidthSetting.CellWidth == ColumnWidth) || (ColumnWidthSetting.Porportional == ColumnWidth))
                {
                    usewidth = col.HeaderCell.Size.Width;
                    // calculate the size of column header cells
                    size = calccellsize(g, col.HeaderCell, headercolstyle, usewidth, colwidthsoverride[i], columnheadercellformat);
                    colwidths.Add(col.Width);                       // otherwise use the data width
                }
                else
                {
                    usewidth = printWidth;
                    // calculate the size of column header cells
                    size = calccellsize(g, col.HeaderCell, headercolstyle, usewidth, colwidthsoverride[i], columnheadercellformat);
                    colwidths.Add(size.Width);
                }

                // accumulate heights, saving largest for data sized option
                if (RowHeightSetting.DataHeight == RowHeight)
                    colheaderheight = (colheaderheight < size.Height ? size.Height : colheaderheight);
                else
                    colheaderheight = col.HeaderCell.Size.Height;
					System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): colheaderheight → {1}", g, colheaderheight));
            }

			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): colheaderheight → {1}", g, colheaderheight));
            //-----------------------------------------------------------------
            // measure the page number
            //-----------------------------------------------------------------

            if (pageno)
            {
                pagenumberHeight = (g.MeasureString("Page", pagenofont, printWidth, pagenumberformat)).Height;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): pagenumberHeight → {1}", g, pagenumberHeight));
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): pagenumberHeight → {1}", g, pagenumberHeight));
            }

            //-----------------------------------------------------------------
            // Calc height of header.
            // Header height is height of page number, title, subtitle and height of column headers
            //-----------------------------------------------------------------
            if (PrintHeader)
            {
                // calculate title and subtitle heights
                titleheight = (g.MeasureString(title, titlefont, printWidth, titleformat)).Height;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): titleheight → {1}", g, titleheight));
                subtitleheight = (g.MeasureString(subtitle, subtitlefont, printWidth, subtitleformat)).Height;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): subtitleheight → {1}", g, subtitleheight));
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): titleheight → {1}", g, titleheight));
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): subtitleheight → {1}", g, subtitleheight));
            }

            //-----------------------------------------------------------------
            // measure the footer, if one is provided. Include the page number if we're printing
            // it on the bottom
            //-----------------------------------------------------------------
            if (PrintFooter)
            {
                if (!String.IsNullOrEmpty(footer))
                {
                    footerHeight += (g.MeasureString(footer, footerfont, printWidth, footerformat)).Height;
                }

                footerHeight += footerspacing;
            }

            //-----------------------------------------------------------------
            // Calculate column widths, adjusting for porportional columns
            // and datawidth columns. Row heights are calculated later
            //-----------------------------------------------------------------
            for (i = 0; i < rowstoprint.Count; i++)
            {
                row = (DataGridViewRow)rowstoprint[i].row;
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): row → {1}", g, row));
				System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): row → {1}", g, row));

                // add row headers if they're visible
                if ((bool)PrintRowHeaders)
                {
                    // provide a default 'blank' value to prevent a 0 length if we're supposed to show
                    // row headers
                    String rowheadertext = String.IsNullOrEmpty(row.HeaderCell.FormattedValue.ToString())
                        ? rowheadercelldefaulttext : row.HeaderCell.FormattedValue.ToString();

                    SizeF rhsize = g.MeasureString(rowheadertext,
                        row.HeaderCell.InheritedStyle.Font);
                    rowheaderwidth = (rowheaderwidth < rhsize.Width) ? rhsize.Width : rowheaderwidth;
					System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): rowheaderwidth → {1}", g, rowheaderwidth));
					System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): rowheaderwidth → {1}", g, rowheaderwidth));
                }

                // calculate widths for each column. We're looking for the largest width needed for
                // all the rows of data.
                for (j = 0; j < colstoprint.Count; j++)
                {
                    col = (DataGridViewColumn)colstoprint[j];

                    //-------------------------------------------------------------
                    // Build string format and cell style 
                    //-------------------------------------------------------------

                    // get gridview style, and override if we have a set style for this column
                    StringFormat currentformat = null;
                    DataGridViewCellStyle colstyle = GetStyle(row, col); // = row.Cells[col.Index].InheritedStyle.Clone();
					System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): colstyle → {1}", g, colstyle));

                    // build the cell style and font 
                    buildstringformat(ref currentformat, colstyle, cellformat.Alignment, cellformat.LineAlignment,
                        cellformat.FormatFlags, cellformat.Trimming);

                    //-------------------------------------------------------------
                    // Calculate and accumulate cell widths and heights
                    //-------------------------------------------------------------
                    float basewidth;

                    // get the default width, depending on overrides. Only calculate data
                    // sizes for DataWidth column setting.
                    if (0 <= colwidthsoverride[j])
                        // set overridden column width
                        basewidth = colwidthsoverride[j];
                    else if ((ColumnWidthSetting.CellWidth == ColumnWidth) || (ColumnWidthSetting.Porportional == ColumnWidth))
                        // set default to same as title cell width
                        basewidth = colwidths[j];
                    else
                    {
                        // limit to one page
                        basewidth = printWidth;
						System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): basewidth → {1}", g, basewidth));
						System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): basewidth → {1}", g, basewidth));

                        // remove padding
                        basewidth -= colstyle.Padding.Left + colstyle.Padding.Right;

                        // calc cell size
                        SizeF size = calccellsize(g, row.Cells[col.Index], colstyle,
                            basewidth, colwidthsoverride[j], currentformat);

                        basewidth = size.Width;
                    }

                    // if width is not overridden and we're using data width then accumulate column widths
                    if (!(0 <= colwidthsoverride[j]) && (ColumnWidthSetting.DataWidth == ColumnWidth))
                        colwidths[j] = colwidths[j] < basewidth ? basewidth : colwidths[j];
                }
            }

            //-----------------------------------------------------------------
            // Break the columns accross page sets. This is the key to printing
            // where the total width is wider than one page.
            //-----------------------------------------------------------------

            // assume everything will fit on one page
            pagesets = new List<PageDef>();
			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): pagesets → {1}", g, pagesets));
			System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): pagesets → {1}", g, pagesets));
            pagesets.Add(new PageDef(PrintMargins, colstoprint.Count, pageWidth));
            int pset = 0;

            // Account for row headers 
            pagesets[pset].coltotalwidth = rowheaderwidth;

            // account for 'fixed' columns - these appear on every pageset
            for (j = 0; j < fixedcolumns.Count; j++)
            {
                int fixedcol = fixedcolumns[j];
                pagesets[pset].columnindex.Add(fixedcol);
                pagesets[pset].colstoprint.Add(colstoprint[fixedcol]);
                pagesets[pset].colwidths.Add(colwidths[fixedcol]);
                pagesets[pset].colwidthsoverride.Add(colwidthsoverride[fixedcol]);
                pagesets[pset].coltotalwidth += (colwidthsoverride[fixedcol] >= 0)
                    ? colwidthsoverride[fixedcol] : colwidths[fixedcol];
            }

            // check on fixed columns
            if (printWidth < (pagesets[pset].coltotalwidth))
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Throwing Exception from measureprintarea(g={0})", g));
                throw new Exception("Fixed column widths exceed the page width.");
            }

            // split remaining columns into page sets
            float columnwidth;
            for (i = 0; i < colstoprint.Count; i++)
            {
                // skip 'fixed' columns since we've already accounted for them
                if (fixedcolumns.Contains(i))
                    continue;

                // get initial column width
                columnwidth = (colwidthsoverride[i] >= 0)
                    ? colwidthsoverride[i] : colwidths[i];

                // See if the column width takes us off the page - Except for the 
                // first column. This will prevent printing an empty page!! Otherwise,
                // columns longer than the page width are printed on their own page
                if (printWidth < (pagesets[pset].coltotalwidth + columnwidth) && i != 0)
                {
                    pagesets.Add(new PageDef(PrintMargins, colstoprint.Count, pageWidth));
                    pset++;

                    // Account for row headers 
                    pagesets[pset].coltotalwidth = rowheaderwidth;

                    // account for 'fixed' columns - these appear on every pageset
                    for (j = 0; j < fixedcolumns.Count; j++)
                    {
                        int fixedcol = fixedcolumns[j];
                        pagesets[pset].columnindex.Add(fixedcol);
                        pagesets[pset].colstoprint.Add(colstoprint[fixedcol]);
                        pagesets[pset].colwidths.Add(colwidths[fixedcol]);
                        pagesets[pset].colwidthsoverride.Add(colwidthsoverride[fixedcol]);
                        pagesets[pset].coltotalwidth += (colwidthsoverride[fixedcol] >= 0)
                            ? colwidthsoverride[fixedcol] : colwidths[fixedcol];
                    }

                    // check on fixed columns
                    if (printWidth < (pagesets[pset].coltotalwidth))
                    {
                        throw new Exception("Fixed column widths exceed the page width.");
                    }
                }

                // update page set definition 
                pagesets[pset].columnindex.Add(i);
                pagesets[pset].colstoprint.Add(colstoprint[i]);
                pagesets[pset].colwidths.Add(colwidths[i]);
                pagesets[pset].colwidthsoverride.Add(colwidthsoverride[i]);
                pagesets[pset].coltotalwidth += columnwidth;
            }

            // for right to left language, reverse the column order for each page set
            if (RightToLeft.Yes == dgv.RightToLeft)
            {
                for (pset = 0; pset < pagesets.Count; pset++)
                {
                    pagesets[pset].columnindex.Reverse();
                    pagesets[pset].colstoprint.Reverse();
                    pagesets[pset].colwidths.Reverse();
                    pagesets[pset].colwidthsoverride.Reverse();
                }
            }

            for (i = 0; i < pagesets.Count; i++)
            {
                PageDef pageset = pagesets[i];
                if (EnableLogging)
                {
                    String columnlist = "";
					System.Diagnostics.Trace.WriteLine(string.Format("measureprintarea(g={0}): columnlist → {1}", g, columnlist));

                    Logger.LogInfoMsg(String.Format("PageSet {0} Information ----------------------------------------------", i));

                    // list out all the columns printed on this page since we may have fixed columns to account for
                    for (int k = 0; k < pageset.colstoprint.Count; k++)
                        columnlist = String.Format("{0},{1}", columnlist,
                            ((DataGridViewColumn)(pageset.colstoprint[k])).Index);
                    Logger.LogInfoMsg(String.Format("Measured columns {0}", columnlist.Substring(1)));
                    columnlist = "";

                    // list original column widths for this page
                    for (int k = 0; k < pageset.colstoprint.Count; k++)
                        columnlist = String.Format("{0},{1}", columnlist, pageset.colwidths[k]);
                    Logger.LogInfoMsg(String.Format("Original Column Widths: {0}", columnlist.Substring(1)));
                    columnlist = "";

                    // list column width override values
                    for (int k = 0; k < pageset.colstoprint.Count; k++)
                        columnlist = String.Format("{0},{1}", columnlist, pageset.colwidthsoverride[k]);
                    Logger.LogInfoMsg(String.Format("Overridden Column Widths: {0}", columnlist.Substring(1)));
                    columnlist = "";
                }

                //-----------------------------------------------------------------
                // Adjust column widths and table margins for each page
                //-----------------------------------------------------------------
                AdjustPageSets(g, pageset);

                //-----------------------------------------------------------------
                // Log Pagesets 
                //-----------------------------------------------------------------
                if (EnableLogging)
                {
                    String columnlist = "";

                    // list final column widths for this page
                    for (int k = 0; k < pageset.colstoprint.Count; k++)
                        columnlist = String.Format("{0},{1}", columnlist, pageset.colwidths[k]);
                    Logger.LogInfoMsg(String.Format("Final Column Widths: {0}", columnlist.Substring(1)));
                    columnlist = "";

                    Logger.LogInfoMsg(String.Format("pageset print width is {0}, total column width to be printed is {1}",
                        pageset.printWidth, pageset.coltotalwidth));
                }
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting measureprintarea(g={0})", g));
        }

        /// <summary>
        /// Adjust column widths for fixed and porportional columns, set the 
        /// margins to enforce the selected tablealignment.
        /// </summary>
        /// <param name="g">The graphics context for all measurements</param>
        /// <param name="pageset">The pageset to adjust</param>
        private void AdjustPageSets(Graphics g, PageDef pageset)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering AdjustPageSets(g={0},pageset={1})", g, pageset));
            int i;
            float fixedcolwidth = rowheaderwidth;
            float remainingcolwidth = 0;
            float ratio;

            //-----------------------------------------------------------------
            // Adjust the column widths in the page set to their final values,
            // accounting for overridden widths and porportional column stretching
            //-----------------------------------------------------------------

            // calculate the amount of space reserved for fixed width columns
            for (i = 0; i < pageset.colwidthsoverride.Count; i++)
                if (pageset.colwidthsoverride[i] >= 0)
                    fixedcolwidth += pageset.colwidthsoverride[i];

            // calculate the amount space requested for non-overridden columns
            for (i = 0; i < pageset.colwidths.Count; i++)
                if (pageset.colwidthsoverride[i] < 0)
                    remainingcolwidth += pageset.colwidths[i];

            // calculate the ratio for porportional columns, use 1 for 
            // non-overridden columns or not porportional
            if ((porportionalcolumns || ColumnWidthSetting.Porportional == ColumnWidth) &&
                0 < remainingcolwidth)
                ratio = ((float)printWidth - fixedcolwidth) / (float)remainingcolwidth;
            else
                ratio = (float)1.0;
				System.Diagnostics.Trace.WriteLine(string.Format("AdjustPageSets(g={0},pageset={1}): ratio → {2}", g, pageset, ratio));

            // reset all column widths for override and/or porportionality. coltotalwidth
			System.Diagnostics.Trace.WriteLine(string.Format("AdjustPageSets(g={0},pageset={1}): ratio → {2}", g, pageset, ratio));
            // for each pageset should be <= pageWidth
            pageset.coltotalwidth = rowheaderwidth;
			System.Diagnostics.Trace.WriteLine(string.Format("AdjustPageSets(g={0},pageset={1}): pageset.coltotalwidth → {2}", g, pageset, pageset.coltotalwidth));
            for (i = 0; i < pageset.colwidths.Count; i++)
            {
                if (pageset.colwidthsoverride[i] >= 0)
                    // use set width
                    pageset.colwidths[i] = pageset.colwidthsoverride[i];
                else if (ColumnWidthSetting.Porportional == ColumnWidth)
                    // change the width by the ratio
                    pageset.colwidths[i] = pageset.colwidths[i] * ratio;
                else if (pageset.colwidths[i] > printWidth - pageset.coltotalwidth)
                    pageset.colwidths[i] = printWidth - pageset.coltotalwidth;

                //recalculate any rows that need to flow down the page
                RecalcRowHeights(g, pageset.columnindex[i], pageset.colwidths[i]);

                pageset.coltotalwidth += pageset.colwidths[i];

            }

            //-----------------------------------------------------------------
            // Table Alignment - now that we have the column widths established
            // we can reset the table margins to get left, right and centered
            // for the table on the page
            //-----------------------------------------------------------------

            // Reset Print Margins based on table alignment
            if (Alignment.Left == tablealignment)
            {
                // Bias table to the left by setting "right" value
                pageset.margins.Right = pageWidth - pageset.margins.Left - (int)pageset.coltotalwidth;
                if (0 > pageset.margins.Right) pageset.margins.Right = 0;
            }
            else if (Alignment.Right == tablealignment)
            {
                // Bias table to the right by setting "left" value
                pageset.margins.Left = pageWidth - pageset.margins.Right - (int)pageset.coltotalwidth;
                if (0 > pageset.margins.Left) pageset.margins.Left = 0;
            }
            else if (Alignment.Center == tablealignment)
            {
                // Bias the table to the center by setting left and right equal
                pageset.margins.Left = (pageWidth - (int)pageset.coltotalwidth) / 2;
                if (0 > pageset.margins.Left) pageset.margins.Left = 0;
                pageset.margins.Right = pageset.margins.Left;
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting AdjustPageSets(g={0},pageset={1})", g, pageset));
        }

        /// <summary>
        /// Set page breaks for the rows to be printed, and count total pages
        /// </summary>
        private int Pagination()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering Pagination()");
            float pos = 0;
            paging newpage = paging.keepgoing;
			System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): newpage → {0}", newpage));
			System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): pos → {0}", pos));

            //// if we're printing by pages, the total pages is the last page to 
            //// print
            //if (toPage < maxPages)
            //    return toPage;

            // Start counting pages at 1
            CurrentPage = 1;
			System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): CurrentPage → {0}", CurrentPage));

            // Calculate where to stop printing the grid - count up from the bottom of the page.
            staticheight = pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom; //PrintMargins.Bottom;
			System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): staticheight → {0}", staticheight));

            // add in the page number height - doesn't matter at this point if it's printing on top or bottom
            staticheight -= PageNumberHeight;

            // Calculate where to start printing the grid for page 1
            pos = PrintMargins.Top + HeaderHeight;

            // set starting value for 'break on value change' column
            if (!String.IsNullOrEmpty(breakonvaluechange))
            {
                oldvalue = rowstoprint[0].row.Cells[breakonvaluechange].EditedFormattedValue;
				System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): oldvalue → {0}", oldvalue));
				System.Diagnostics.Trace.WriteLine(string.Format("Pagination(): oldvalue → {0}", oldvalue));
            }

            // if we're printing by rows, sum up rowheights until we're done.
            for (int currentrow = 0; currentrow < (rowstoprint.Count); currentrow++)
            {
                // end of page: Count the page and reset to top of next page
                if (pos + rowstoprint[currentrow].height >= staticheight)
                {
                    newpage = paging.outofroom;
                }

                // if we're breaking on value change in a column then watch that column
                if ((!String.IsNullOrEmpty(breakonvaluechange)) &&
                    (!oldvalue.Equals(rowstoprint[currentrow].row.Cells[breakonvaluechange].EditedFormattedValue)))
                {
                    newpage = paging.datachange;
                    oldvalue = rowstoprint[currentrow].row.Cells[breakonvaluechange].EditedFormattedValue;
                }

                // if we need to start a new page, count it and reset counters
                if (newpage != paging.keepgoing)
                {
                    // note page break
                    rowstoprint[currentrow].pagebreak = true;

                    // count the page
                    CurrentPage++;

                    // if we're printing by pages, stop when we pass our limit
                    if (CurrentPage > toPage)
                    {
                        // we're done
                        return toPage;
                    }

                    // reset the counter - depending on setting
                    if (KeepRowsTogether
                        || newpage == paging.datachange
                        || (newpage == paging.outofroom && (staticheight - pos) < KeepRowsTogetherTolerance))
                    {
                        // if we are keeping rows together and too little would be showing, put whole row on next page
                        pos = rowstoprint[currentrow].height;
                    }
                    else
                    {
                        // note page split
                        rowstoprint[currentrow].splitrow = true;

                        // if we're not keeping rows together, only put remainder on next page
                        pos = pos + rowstoprint[currentrow].height - staticheight;
                    }

                    // Recalculate where to stop printing the grid because available space can change w/ dynamic header/footers.
                    staticheight = pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom; //PrintMargins.Bottom;

                    // add in the page number height - doesn't matter at this point if it's printing on top or bottom
                    staticheight += PageNumberHeight;


                    // account for static space at the top of the page
                    pos += PrintMargins.Top + HeaderHeight + PageNumberHeight;
                }
                else
                {
                    // add row space
                    pos += rowstoprint[currentrow].height;
                }

                // reset flag
                newpage = paging.keepgoing;
            }

            // return counted pages
			System.Diagnostics.Trace.WriteLine("<<< Exiting Pagination()");
            return CurrentPage;
        }

        /// <summary>
        /// Check for more pages. This is called at the end of printing a page set.
        /// If there's another page set to print, we return true.
        /// </summary>
        private bool DetermineHasMorePages()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering DetermineHasMorePages()");
            currentpageset++;
            if (currentpageset < pagesets.Count)
            {
                //currentpageset--;   // decrement back to a valid pageset number
                return true;        // tell the caller we're through.
            }
            else {
				System.Diagnostics.Trace.WriteLine("<<< Exiting DetermineHasMorePages()");
                return false;
            }
        }

        /// <summary>
        /// This routine prints one page. It will skip non-printable pages if the user 
        /// selected the "some pages" option on the print dialog. This is called during 
        /// the Print event.
        /// </summary>
        /// <param name="g">Graphics object to print to</param>
        private bool PrintPage(Graphics g)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintPage(g={0})", g));
            // for tracing and logging purposes
            int firstrow = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): firstrow → {1}", g, firstrow));

            // flag for continuing or ending print process
            bool HasMorePages = false;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): HasMorePages → {1}", g, HasMorePages));

            // flag for handling printing some pages rather than all
            bool printthispage = false;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): printthispage → {1}", g, printthispage));

            // current printing position within one page
            float printpos = pagesets[currentpageset].margins.Top;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): printpos → {1}", g, printpos));

            // increment page number & check page range
            CurrentPage++;
            if (EnableLogging) Logger.LogInfoMsg(String.Format("Print Page processing page {0} -----------------------", CurrentPage));
            if ((CurrentPage >= fromPage) && (CurrentPage <= toPage))
                printthispage = true;

            // calculate the static vertical space available - this is where we stop printing rows
            // Note: leave room for the page number if it's on the bottom
            staticheight = pageHeight - FooterHeight - pagesets[currentpageset].margins.Bottom;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): staticheight → {1}", g, staticheight));
            if (!pagenumberontop)
                staticheight -= PageNumberHeight;

            // count space used as we work our way down the page
            float used = 0;

            // current row information block
            rowdata thisrow = null;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): thisrow → {1}", g, thisrow));

            // next row (lookahead) information block
            rowdata nextrow = null;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): nextrow → {1}", g, nextrow));

            //-----------------------------------------------------------------
            // scan down heights until we're off this (non-printing) page
            //-----------------------------------------------------------------

            while (!printthispage)
            {
                if (EnableLogging) Logger.LogInfoMsg(String.Format("Print Page skipping page {0} part {1}", CurrentPage, currentpageset + 1));

                // calculate and increment over the page we're not printing
                printpos = pagesets[currentpageset].margins.Top + HeaderHeight + PageNumberHeight;

                // are we done with this page?
                bool pagecomplete = false;
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): pagecomplete → {1}", g, pagecomplete));
                currentrow = lastrowprinted + 1;
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): currentrow → {1}", g, currentrow));
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): currentrow → {1}", g, currentrow));

                // for logging
                firstrow = currentrow;

                do
                {
                    thisrow = rowstoprint[currentrow];

                    // this is how much space this row will use on this page
                    used = (thisrow.height - rowstartlocation) > (staticheight - printpos)
                            ? (staticheight - printpos) : thisrow.height - rowstartlocation;
                    printpos += used;

                    // Now, look at the next row and start checking on whether or not we're out of room & need to count a page
                    lastrowprinted++;
                    currentrow++;
                    nextrow = (currentrow < rowstoprint.Count) ? rowstoprint[currentrow] : null;
                    if (null != nextrow && nextrow.pagebreak) // pagebreak before the next row
                    {
                        pagecomplete = true;

                        if (nextrow.splitrow)
                        {
                            // account for the partial row that would go on this page
                            rowstartlocation += (nextrow.height - rowstartlocation) > (staticheight - printpos)
                                ? (staticheight - printpos) : nextrow.height - rowstartlocation;
                        }
                    }
                    else
                    {
                        // completed a row, so reset startlocation and count this row.
                        rowstartlocation = 0;
						System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): rowstartlocation → {1}", g, rowstartlocation));
						System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): rowstartlocation → {1}", g, rowstartlocation));
                    }

                    // if we're out of data (no partial rows and no more rows)
                    if ((0 == rowstartlocation) && lastrowprinted >= rowstoprint.Count - 1)
                        pagecomplete = true;

                } while (!pagecomplete);

                // log rows skipped
                if (EnableLogging) Logger.LogInfoMsg(String.Format("Print Page skipped rows {0} to {1}", firstrow, currentrow));

                // skip to the next page & see if it's in the print range
                CurrentPage++;

                if ((CurrentPage >= fromPage) && (CurrentPage <= toPage))
                    printthispage = true;

                // partial row means more to print
                if (0 != rowstartlocation)
                {
                    // we're not done with this row yet
                    HasMorePages = true;
                }
                // done with this page set so see if there are any more pagesets to print
                else if ((lastrowprinted >= rowstoprint.Count - 1) || (CurrentPage > toPage))
                {
                    // reset for next pageset or tell the caller we're complete
                    HasMorePages = DetermineHasMorePages();

                    // reset counters since we'll go through this twice if we print from preview
                    lastrowprinted = -1;
					System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): lastrowprinted → {1}", g, lastrowprinted));
					System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): lastrowprinted → {1}", g, lastrowprinted));
                    CurrentPage = 0;
					System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): CurrentPage → {1}", g, CurrentPage));

					System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): CurrentPage → {1}", g, CurrentPage));
                    return HasMorePages;
                }
            }

            if (EnableLogging)
            {
                Logger.LogInfoMsg(String.Format("Print Page printing page {0} part {1}", CurrentPage, currentpageset + 1));
                var m = pagesets[currentpageset].margins;
                Logger.LogInfoMsg(String.Format("Current Margins are {0}, {1}, {2}, {3}", m.Left, m.Right, m.Top, m.Bottom));
            }

            //-----------------------------------------------------------------
            // print statically located images
            //-----------------------------------------------------------------

            // print any "absolute" images so that anything else we print will be 'on top'
            ImbeddedImageList.Where(p => p.ImageLocation == Location.Absolute).DrawImbeddedImage(g, pagesets[currentpageset].printWidth,
                pageHeight, pagesets[currentpageset].margins);

            //-----------------------------------------------------------------
            // print headers
            //-----------------------------------------------------------------

            // reset printpos as it may have changed during the 'skip pages' routine just above.
            printpos = pagesets[currentpageset].margins.Top;

            // Skip headers if the flag is false
            if (PrintHeader)
            {
                // print any "header" images so that anything else we print will be 'on top'
                ImbeddedImageList.Where(p => p.ImageLocation == Location.Header).DrawImbeddedImage(g, pagesets[currentpageset].printWidth,
                    pageHeight, pagesets[currentpageset].margins);

                // print page number if user selected it
                if (pagenumberontop)
                {
                    printpos = PrintPageNo(g, printpos);
                }

                // print title if provided, & we're not skipping it
                if (0 != TitleHeight && !String.IsNullOrEmpty(title))
                    printsection(g, ref printpos, title, titlefont,
                        titlecolor, titleformat, overridetitleformat,
                        pagesets[currentpageset],
						System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting printsection(g={0},pos={1},text={2},font={3},color={4},format={5},useroverride={6},pageset={7},background={8},border={9})", g, pos, text, font, color, format, useroverride, pageset, background, border));
                        titlebackground, titleborder);

                // account for title spacing
                printpos += TitleHeight;

                // print subtitle if provided
                if (0 != SubTitleHeight && !String.IsNullOrEmpty(subtitle))
                    printsection(g, ref printpos, subtitle, subtitlefont,
                        subtitlecolor, subtitleformat, overridesubtitleformat,
                        pagesets[currentpageset],
                        subtitlebackground, subtitleborder);

                // account for subtitle spacing
                printpos += SubTitleHeight;
            }

            // print the column headers or not based on our processing flag
            if ((bool)PrintColumnHeaders)
            {
                // print column headers
                printcolumnheaders(g, ref printpos, pagesets[currentpageset]);
            }

            //-----------------------------------------------------------------
            // print rows until the page is complete
            //-----------------------------------------------------------------
            bool continueprinting = true;
			System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): continueprinting → {1}", g, continueprinting));
            currentrow = lastrowprinted + 1;

            // for logging
            firstrow = currentrow;

            if (currentrow >= rowstoprint.Count)
            {
                // indicate that we're done printing 
                continueprinting = false;
            }

            while (continueprinting)
            {
                thisrow = rowstoprint[currentrow];

                // print the part of the row that we can, and accumulate the space used
                used = printrow(g, printpos, (DataGridViewRow)(thisrow.row),
                    pagesets[currentpageset], rowstartlocation);
                printpos += used;

                // Now, start checking on whether or not to print the next row 
                // (or if we even have a next row)
                lastrowprinted++;
                currentrow++;
                nextrow = (currentrow < rowstoprint.Count) ? rowstoprint[currentrow] : null;
                if (null != nextrow && nextrow.pagebreak)
                {
                    continueprinting = false;

                    // print a partial row before breaking
                    if (nextrow.splitrow)
                    {
                        // print what we can on this page, print the remainder on the next page
                        rowstartlocation += printrow(g, printpos, (DataGridViewRow)(nextrow.row),
                            pagesets[currentpageset], rowstartlocation);
                    }
                }
                else
                {
                    // completed a row, so reset startlocation.
                    rowstartlocation = 0;
                }

                // if we're out of data (no partial rows and no more rows)
                if ((0 == rowstartlocation) && lastrowprinted >= rowstoprint.Count - 1)
                    continueprinting = false;

            }

            // log rows skipped
            if (EnableLogging)
            {
                Logger.LogInfoMsg(String.Format("Print Page printed rows {0} to {1}", firstrow, currentrow));
                PageDef pageset = pagesets[currentpageset];
                String columnlist = "";
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPage(g={0}): columnlist → {1}", g, columnlist));

                // list out all the columns printed on this page since we may have fixed columns to account for
                for (int i = 0; i < pageset.colstoprint.Count; i++)
                    columnlist = String.Format("{0},{1}", columnlist,
                        ((DataGridViewColumn)(pageset.colstoprint[i])).Index);

                Logger.LogInfoMsg(String.Format("Print Page printed columns {0}", columnlist.Substring(1)));
            }

            //-----------------------------------------------------------------
            // print footer
            //-----------------------------------------------------------------
            if (PrintFooter)
            {
                // print any "footer" images so that anything else we print will be 'on top'
                ImbeddedImageList.Where(p => p.ImageLocation == Location.Footer).DrawImbeddedImage(g, pagesets[currentpageset].printWidth,
                    pageHeight, pagesets[currentpageset].margins);

                //Note: need to force printpos to the bottom of the page
                // as we may have run out of data anywhere on the page
                printpos = pageHeight - footerHeight - pagesets[currentpageset].margins.Bottom;  // - margins.Top

                // add spacing
                printpos += footerspacing;

                // print the page number if it's on the bottom.
                if (!pagenumberontop)
                {
                    printpos = PrintPageNo(g, printpos);
                }

                if (0 != FooterHeight)
                    printfooter(g, ref printpos, pagesets[currentpageset]);
            }

            //-----------------------------------------------------------------
            // bottom check, see if this is the last page to print
            //-----------------------------------------------------------------

            // partial row means more to print
            if (0 != rowstartlocation)
            {
                // we're not done with this row yet
                HasMorePages = true;
            }

            // done with this page set so see if there are any more pagesets to print
            if ((CurrentPage >= toPage) || (lastrowprinted >= rowstoprint.Count - 1))
            {
                // reset for next pageset or tell the caller we're complete
                HasMorePages = DetermineHasMorePages();

                // reset counters since we'll go through this twice if we print from preview
                rowstartlocation = 0;
                lastrowprinted = -1;
                CurrentPage = 0;
            }
            else
            {
                // we're not done yet
                HasMorePages = true;
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintPage(g={0})", g));
            return HasMorePages;
        }

        /// <summary>
        /// Print the page number
        /// </summary>
        /// <param name="g"></param>
        /// <param name="printpos"></param>
        /// <returns></returns>
        private float PrintPageNo(Graphics g, float printpos)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering PrintPageNo(g={0},printpos={1})", g, printpos));
            if (pageno)
            {
                String pagenumber = pagetext + CurrentPage.ToString(CultureInfo.CurrentCulture);
				System.Diagnostics.Trace.WriteLine(string.Format("PrintPageNo(g={0},printpos={1}): pagenumber → {2}", g, printpos, pagenumber));
                if (showtotalpagenumber)
                {
                    pagenumber += pageseparator + totalpages.ToString(CultureInfo.CurrentCulture);
                }
                if (1 < pagesets.Count)
                    pagenumber += parttext + (currentpageset + 1).ToString(CultureInfo.CurrentCulture);

                // ... then print it
                printsection(g, ref printpos,
                    pagenumber, pagenofont, pagenocolor, pagenumberformat,
                    overridepagenumberformat, pagesets[currentpageset],
                    null, null);

                // if the page number is not on a separate line, don't "use up" it's vertical space
                if (pagenumberonseparateline)
                    printpos += pagenumberHeight;
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting PrintPageNo(g={0},printpos={1})", g, printpos));
            return printpos;
        }

        /// <summary>
        /// Print a header or footer section. Used for page numbers and titles
        /// </summary>
        /// <param name="g">Graphic context to print in</param>
        /// <param name="pos">Track vertical space used; 'y' location</param>
        /// <param name="text">String to print</param>
        /// <param name="font">Font to use for printing</param>
        /// <param name="color">Color to print in</param>
        /// <param name="format">String format for text</param>
        /// <param name="useroverride">True if the user overrode the alignment or flags</param>
        /// <param name="margins">The table's print margins</param>
        /// <param name="background">Background fill for the section; may be null for no background</param>
        /// <param name="border">Border for the section; may be null for no border</param>
        private void printsection(Graphics g, ref float pos, string text,
            Font font, Color color, StringFormat format, bool useroverride, PageDef pageset,
            Brush background, Pen border)
        {
            // measure string
            SizeF printsize = g.MeasureString(text, font, pageset.printWidth, format);

            // build area to print within
            RectangleF printarea = new RectangleF((float)pageset.margins.Left, pos, (float)pageset.printWidth,
               printsize.Height);

            // draw a background, if a Brush has been provided
            if (null != background)
            {
                g.FillRectangle(background, printarea);
            }

            // draw a border, if a Pen has been provided
            if (null != border)
            {
                g.DrawRectangle(border, printarea.X, printarea.Y, printarea.Width, printarea.Height);
            }

            // do the actual print
            g.DrawString(text, font, new SolidBrush(color), printarea, format);
        }

        /// <summary>
        /// Print the footer. This handles the footer spacing, and printing the page number
        /// at the bottom of the page (if the page number is not in the header).
        /// </summary>
        /// <param name="g">Graphic context to print in</param>
        /// <param name="pos">Track vertical space used; 'y' location</param>
        /// <param name="margins">The table's print margins</param>
        private void printfooter(Graphics g, ref float pos, PageDef pageset)
        {
		System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering printfooter(g={0},pos={1},pageset={2})", g, pos, pageset));

            // print the footer
            printsection(g, ref pos, footer, footerfont, footercolor, footerformat,
                overridefooterformat, pageset, footerbackground, footerborder);
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting printfooter(g={0},pos={1},pageset={2})", g, pos, pageset));
        }

        /// <summary>
        /// Print the column headers. Most printing format info is retrieved from the 
        /// source DataGridView.
        /// </summary>
        /// <param name="g">Graphics Context to print within</param>
        /// <param name="pos">Track vertical space used; 'y' location</param>
        /// <param name="pageset">Current pageset - defines columns and margins</param>
        private void printcolumnheaders(Graphics g, ref float pos, PageDef pageset)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering printcolumnheaders(g={0},pos={1},pageset={2})", g, pos, pageset));
            // track printing location accross the page. start position is hard left,
            // adjusted for the row headers. Note rowheaderwidth is 0 if row headers are not printed
            float xcoord = pageset.margins.Left + rowheaderwidth;

            // set the pen for drawing the grid lines
            Pen lines = new Pen(dgv.GridColor, 1);
			System.Diagnostics.Trace.WriteLine(string.Format("printcolumnheaders(g={0},pos={1},pageset={2}): lines → {3}", g, pos, pageset, lines));

            //-----------------------------------------------------------------
            // Print the column headers
            //-----------------------------------------------------------------
            DataGridViewColumn col;
            for (int i = 0; i < pageset.colstoprint.Count; i++)
            {
                col = (DataGridViewColumn)pageset.colstoprint[i];
				System.Diagnostics.Trace.WriteLine(string.Format("printcolumnheaders(g={0},pos={1},pageset={2}): col → {3}", g, pos, pageset, col));
				System.Diagnostics.Trace.WriteLine(string.Format("printcolumnheaders(g={0},pos={1},pageset={2}): col → {3}", g, pos, pageset, col));

                // calc cell width, account for columns larger than the print area!
                float cellwidth = (pageset.colwidths[i] > pageset.printWidth - rowheaderwidth ?
                    pageset.printWidth - rowheaderwidth : pageset.colwidths[i]);

                // get column style
                DataGridViewCellStyle style = col.HeaderCell.InheritedStyle.Clone();
				System.Diagnostics.Trace.WriteLine(string.Format("printcolumnheaders(g={0},pos={1},pageset={2}): style → {3}", g, pos, pageset, style));
                if (ColumnHeaderStyles.ContainsKey(col.Name))
                {
                    style = ColumnHeaderStyles[col.Name];
                }

                // set print area for this individual cell, account for cells larger
                // than the print area!
                RectangleF cellprintarea = new RectangleF(xcoord, pos, cellwidth, colheaderheight);
				System.Diagnostics.Trace.WriteLine(string.Format("printcolumnheaders(g={0},pos={1},pageset={2}): cellprintarea → {3}", g, pos, pageset, cellprintarea));

                DrawCell(g, cellprintarea, style, col.HeaderCell, 0, columnheadercellformat, lines);

                xcoord += pageset.colwidths[i];
            }

            // all done, consume "used" vertical space, including space for border lines
            pos += colheaderheight +
                (dgv.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None ? lines.Width : 0);
				System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting printcolumnheaders(g={0},pos={1},pageset={2})", g, pos, pageset));
        }

        /// <summary>
        /// Print one row of the DataGridView. Most printing format info is retrieved
        /// from the DataGridView.
        /// </summary>
        /// <param name="g">Graphics Context to print within</param>
        /// <param name="pos">Track vertical space used; 'y' location</param>
        /// <param name="row">The row that will be printed</param>
        /// <param name="pageset">Current Pageset - defines columns and margins</param>
        /// <param name="startline">Line no. in row to start printing text at</param>
        private float printrow(Graphics g, float finalpos, DataGridViewRow row, PageDef pageset,
            float startlocation)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4})", g, finalpos, row, pageset, startlocation));
            // track printing location accross the page
            float xcoord = pageset.margins.Left;
            float pos = finalpos;

            // set the pen for drawing the grid lines
            Pen lines = new Pen(dgv.GridColor, 1);
			System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): lines → {5}", g, finalpos, row, pageset, startlocation, lines));

            // calc row width, account for columns wider than the print area!
            float rowwidth = (pageset.coltotalwidth > pageset.printWidth ? pageset.printWidth : pageset.coltotalwidth);
			System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): rowwidth → {5}", g, finalpos, row, pageset, startlocation, rowwidth));

            // calc row heigth in pixels to print
            float rowheight = (rowstoprint[currentrow].height - startlocation) > (staticheight - pos)
                ? (staticheight - pos) : rowstoprint[currentrow].height - startlocation;

            //-----------------------------------------------------------------
            // Print Row background
            //-----------------------------------------------------------------

            // get current row style, and current header style
            DataGridViewCellStyle rowstyle = row.InheritedStyle.Clone();
			System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): rowstyle → {5}", g, finalpos, row, pageset, startlocation, rowstyle));
            DataGridViewCellStyle headerstyle = row.HeaderCell.InheritedStyle.Clone();
			System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): headerstyle → {5}", g, finalpos, row, pageset, startlocation, headerstyle));

            // define print rectangle
            RectangleF printarea = new RectangleF(xcoord, pos, rowwidth,
                rowheight);

            // fill in the row background as the default color
            g.FillRectangle(new SolidBrush(rowstyle.BackColor), printarea);

            //-----------------------------------------------------------------
            // Print the Row Headers, if they are visible
            //-----------------------------------------------------------------
            if ((bool)PrintRowHeaders)
            {
                // set print area for this individual cell
                RectangleF headercellprintarea = new RectangleF(xcoord, pos,
                    rowheaderwidth, rowheight);

                DrawCell(g, headercellprintarea, headerstyle, row.HeaderCell, startlocation,
                    rowheadercellformat, lines);

                // track horizontal space used
                xcoord += rowheaderwidth;
            }

            //-----------------------------------------------------------------
            // Print the row: write and draw each cell
            //-----------------------------------------------------------------
            DataGridViewColumn col;
            for (int i = 0; i < pageset.colstoprint.Count; i++)
            {
                // access the cell and column being printed
                col = (DataGridViewColumn)pageset.colstoprint[i];
				System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): col → {5}", g, finalpos, row, pageset, startlocation, col));
				System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): col → {5}", g, finalpos, row, pageset, startlocation, col));
                DataGridViewCell cell = row.Cells[col.Index];

                // calc cell width, account for columns larger than the print area!
                float cellwidth = (pageset.colwidths[i] > pageset.printWidth - rowheaderwidth ?
                    pageset.printWidth - rowheaderwidth : pageset.colwidths[i]);

                // SLG 01112010 - only draw columns with an actual width
                if (cellwidth > 0)
                {
                    // get DGV column style and see if we have an override for this column
                    StringFormat finalformat = null;
                    Font cellfont = null;
					System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): cellfont → {5}", g, finalpos, row, pageset, startlocation, cellfont));
                    DataGridViewCellStyle colstyle = GetStyle(row, col); // = row.Cells[col.Index].InheritedStyle.Clone(); 
					System.Diagnostics.Trace.WriteLine(string.Format("printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4}): colstyle → {5}", g, finalpos, row, pageset, startlocation, colstyle));

                    // set string format
                    buildstringformat(ref finalformat, colstyle, cellformat.Alignment, cellformat.LineAlignment,
                        cellformat.FormatFlags, cellformat.Trimming);
                    cellfont = colstyle.Font;

                    // set overall print area for this individual cell 
                    RectangleF cellprintarea = new RectangleF(xcoord, pos, cellwidth,
                        rowheight);

                    DrawCell(g, cellprintarea, colstyle, cell, startlocation, finalformat, lines);
                }
                // track horizontal space used
                xcoord += pageset.colwidths[i];
            }

            //-----------------------------------------------------------------
            // All done with this row, consume "used" vertical space
            //-----------------------------------------------------------------
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting printrow(g={0},finalpos={1},row={2},pageset={3},startlocation={4})", g, finalpos, row, pageset, startlocation));
            return rowheight;
        }

        /// <summary>
        /// Allow override of cell drawing. This is to support grids that have onPaint
        /// overridden to do things like images in header rows and vertical printing
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rowindex"></param>
        /// <param name="columnindex"></param>
        /// <param name="rectf"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        Boolean DrawOwnerDrawCell(Graphics g, int rowindex, int columnindex, RectangleF rectf,
            DataGridViewCellStyle style)
        {
            DGVCellDrawingEventArgs args = new DGVCellDrawingEventArgs(g, rectf, style,
                rowindex, columnindex);
            OnCellOwnerDraw(args);
            return args.Handled;
        }

        /// <summary>
        /// Draw a cell. Used for column and row headers and body cells.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cellprintarea"></param>
        /// <param name="style"></param>
        /// <param name="cell"></param>
        /// <param name="startlocation"></param>
        /// <param name="cellformat"></param>
        /// <param name="lines"></param>
        void DrawCell(Graphics g, RectangleF cellprintarea, DataGridViewCellStyle style,
            DataGridViewCell cell, float startlocation, StringFormat cellformat, Pen lines)
        {
            // Draw the cell if it's not overridden by ownerdrawing
            if (!DrawOwnerDrawCell(g, cell.RowIndex, cell.ColumnIndex, cellprintarea, style))
            {
                // save original clipping bounds
                RectangleF clip = g.ClipBounds;

                // fill in the full cell background - using the selected style
                //g.FillRectangle(new SolidBrush(colstyle.BackColor), cellprintarea);
                g.FillRectangle(new SolidBrush(style.BackColor), cellprintarea);

                // reset print area for this individual cell, adjusting 'inward' for cell padding
                RectangleF paddedcellprintarea = new RectangleF(cellprintarea.X + style.Padding.Left,
                    cellprintarea.Y + style.Padding.Top,
                    cellprintarea.Width - style.Padding.Right - style.Padding.Left,
                    cellprintarea.Height - style.Padding.Bottom - style.Padding.Top);

                // set clipping to current print area - i.e. our cell
                g.SetClip(cellprintarea);

                // define the *actual* print area based on the given startlocation. Offset the start by 
                // minus the start location, increase the print area height by the startlocation
                RectangleF actualprint = new RectangleF(paddedcellprintarea.X, paddedcellprintarea.Y - startlocation,
                    paddedcellprintarea.Width, paddedcellprintarea.Height + startlocation);

                // draw content based on cell style, but only for "body" cells
                if (0 <= cell.RowIndex && 0 <= cell.ColumnIndex)
                {
                    if ("DataGridViewImageCell" == dgv.Columns[cell.ColumnIndex].CellType.Name)
                    {
                        // draw the image for image cells
                        DrawImageCell(g, (DataGridViewImageCell)cell, actualprint);
                    }
                    else if ("DataGridViewCheckBoxCell" == dgv.Columns[cell.ColumnIndex].CellType.Name)
                    {
                        // draw a checkbox for checkbox cells
                        DrawCheckBoxCell(g, (DataGridViewCheckBoxCell)cell, actualprint);
                    }
                    else
                    {
                        // this handles drawing for textbox, button, combobox, and link cell types.
                        // currently these are not drawn as "controls" for performance reasons.

                        // draw the text for the cell at the row / col intersection
                        g.DrawString(cell.FormattedValue.ToString(), style.Font,
                            new SolidBrush(style.ForeColor), actualprint, cellformat);
                    }
                }
                else
                {
                    // draw the text for the cell at the row / col intersection
                    g.DrawString(cell.FormattedValue.ToString(), style.Font,
                        new SolidBrush(style.ForeColor), actualprint, cellformat);
                }

                // reset clipping bounds to "normal"
                g.SetClip(clip);

                // draw the borders - default to the dgv's border setting, and use unpadded cell print area
                if (dgv.CellBorderStyle != DataGridViewCellBorderStyle.None)
                    g.DrawRectangle(lines, cellprintarea.X, cellprintarea.Y, cellprintarea.Width, cellprintarea.Height);
            }
        }

        /// <summary>
        /// Draw a body cell that is a checkbox
        /// </summary>
        /// <param name="g"></param>
        /// <param name="checkboxcell"></param>
        /// <param name="rectf"></param>
        void DrawCheckBoxCell(Graphics g, DataGridViewCheckBoxCell checkboxcell, RectangleF rectf)
        {
            // create a non-printing graphics context in which to draw the checkbox control
            Image i = new Bitmap((int)rectf.Width, (int)rectf.Height);
            Graphics tg = Graphics.FromImage(i);

            // determine checked or notchecked (or undetermined for tristate checkboxes)
            CheckBoxState state = CheckBoxState.UncheckedNormal;
            if (checkboxcell.ThreeState)
            {
                if (((CheckState)checkboxcell.EditedFormattedValue) == CheckState.Checked)
                    state = CheckBoxState.CheckedNormal;
                else if (((CheckState)checkboxcell.EditedFormattedValue) == CheckState.Indeterminate)
                    state = CheckBoxState.MixedNormal;
            }
            else
            {
                if ((Boolean)checkboxcell.EditedFormattedValue)
                    state = CheckBoxState.CheckedNormal;
            }

            // get the size and location to print the checkbox - currently centered, may change later
            Size size = CheckBoxRenderer.GetGlyphSize(tg, state);
            int x = ((int)rectf.Width - size.Width) / 2;
            int y = ((int)rectf.Height - size.Height) / 2;

            // draw the checkbox in our temporary graphics context
            CheckBoxRenderer.DrawCheckBox(tg, new Point(x, y), state);

            //calculate image drawing origin based on cell alignment
            switch (checkboxcell.InheritedStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    rectf.Y += y;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    rectf.X -= x;
                    rectf.Y += y;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    rectf.X += x;
                    rectf.Y += y;
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    rectf.X -= x;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    rectf.X += x;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    rectf.Y -= y;
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    rectf.X -= x;
                    rectf.Y -= y;
                    break;
                case DataGridViewContentAlignment.TopRight:
                    rectf.X += x;
                    rectf.Y -= y;
                    break;
                case DataGridViewContentAlignment.NotSet:
                    break;
            }

            // now draw the image of the checkbox to our print output
            g.DrawImage(i, rectf);

            // clean up after ourselves
            tg.Dispose();
            i.Dispose();
        }

        /// <summary>
        /// Draw a body cell that has an imbedded image
        /// </summary>
        /// <param name="g"></param>
        /// <param name="imagecell"></param>
        /// <param name="rectf"></param>
        void DrawImageCell(Graphics g, DataGridViewImageCell imagecell, RectangleF rectf)
        {
            // image to draw
            Image img;

            // if we don't actually have a value, then just exit.
            if ((null == imagecell.Value) || (typeof(DBNull) == imagecell.Value.GetType()))
                return;

            // Check on type of image cell value - may not be an actual "image" type
            if ("Image" == imagecell.ValueType.Name)
            {
                // if it's an "image" type, then load it directly
                img = (System.Drawing.Image)imagecell.Value;
            }
            else if ("Byte[]" == imagecell.ValueType.Name)
            {
                // if it's not an "image" type (i.e. loaded from a database to a bound column)
                // convert the underlying byte array to an image
                ImageConverter ic = new ImageConverter();
                img = (Image)ic.ConvertFrom((byte[])imagecell.Value);
            }
            else
                throw new Exception(String.Format("Unknown image cell underlying type: {0} in column {1}",
                    imagecell.ValueType.Name, imagecell.ColumnIndex));

            // clipping bounds. This is the portion of the image to fit into the drawing rectangle
            Rectangle src = new Rectangle();

            // calculate deltas
            int dx = 0;
            int dy = 0;

            // drawn normal size, clipped to cell 
            if ((DataGridViewImageCellLayout.Normal == imagecell.ImageLayout) ||
                (DataGridViewImageCellLayout.NotSet == imagecell.ImageLayout))
            {
                // calculate origin deltas, used to move image
                dx = img.Width - (int)rectf.Width;
                dy = img.Height - (int)rectf.Height;

                // set destination width and height to clip to cell
                if (0 > dx) rectf.Width = src.Width = img.Width; else src.Width = (int)rectf.Width;
                if (0 > dy) rectf.Height = src.Height = img.Height; else src.Height = (int)rectf.Height;

            }
            else if (DataGridViewImageCellLayout.Stretch == imagecell.ImageLayout)
            {
                // stretch image to fit cell size
                src.Width = img.Width;
                src.Height = img.Height;

                // change the origin delta's to 0 so we don't move the image
                dx = 0;
                dy = 0;
            }
            else // DataGridViewImageCellLayout.Zoom
            {
                // scale image to fit in cell
                src.Width = img.Width;
                src.Height = img.Height;

                float vertscale = rectf.Height / src.Height;
                float horzscale = rectf.Width / src.Width;
                float scale;

                // use the smaller scaling factor to ensure the image will fit in the cell
                if (vertscale > horzscale)
                {
                    // use horizontal scale, don't move image horizontally
                    scale = horzscale;
                    dx = 0;
                    dy = (int)((src.Height * scale) - rectf.Height);
                }
                else
                {
                    // use vertical scale, don't move image vertically
                    scale = vertscale;
                    dy = 0;
                    dx = (int)((src.Width * scale) - rectf.Width);
                }

                // set target size to match scaled image
                rectf.Width = src.Width * scale;
                rectf.Height = src.Height * scale;
            }

            //calculate image drawing origin based on origin deltas
            switch (imagecell.InheritedStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    if (0 > dy) rectf.Y -= dy; else src.Y = dy;
                    if (0 > dx) rectf.X -= dx / 2; else src.X = dx / 2;
                    break;
                case DataGridViewContentAlignment.BottomLeft:
                    if (0 > dy) rectf.Y -= dy; else src.Y = dy;
                    src.X = 0;
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    if (0 > dy) rectf.Y -= dy; else src.Y = dy;
                    if (0 > dx) rectf.X -= dx; else src.X = dx;
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    if (0 > dy) rectf.Y -= dy / 2; else src.Y = dy / 2;
                    if (0 > dx) rectf.X -= dx / 2; else src.X = dx / 2;
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    if (0 > dy) rectf.Y -= dy / 2; else src.Y = dy / 2;
                    src.X = 0;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    if (0 > dy) rectf.Y -= dy / 2; else src.Y = dy / 2;
                    if (0 > dx) rectf.X -= dx; else src.X = dx;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    src.Y = 0;
                    if (0 > dx) rectf.X -= dx / 2; else src.X = dx / 2;
                    break;
                case DataGridViewContentAlignment.TopLeft:
                    src.Y = 0;
                    src.X = 0;
                    break;
                case DataGridViewContentAlignment.TopRight:
                    src.Y = 0;
                    if (0 > dx) rectf.X -= dx; else src.X = dx;
                    break;
                case DataGridViewContentAlignment.NotSet:
                    if (0 > dy) rectf.Y -= dy / 2; else src.Y = dy / 2;
                    if (0 > dx) rectf.X -= dx / 2; else src.X = dx / 2;
                    break;
            }

            // Now we can draw our image
            g.DrawImage(img, rectf, src, GraphicsUnit.Pixel);
        }
    }
}
