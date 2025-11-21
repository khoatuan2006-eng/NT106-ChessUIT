using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChessUI
{
    public static class ChessCursors
    {
        // THAY ĐỔI 1: Sử dụng Pack URI tuyệt đối (giống như tệp board.png của bạn)
        public static readonly Cursor WhiteCursor = LoadCursor("pack://application:,,,/ChessUI;component/Assets/CursorW.cur");

        // THAY ĐỔI 2: Sử dụng Pack URI tuyệt đối
        public static readonly Cursor BlackCursor = LoadCursor("pack://application:,,,/ChessUI;component/Assets/CursorB.cur");

        private static Cursor LoadCursor(string fullPackUri)
        {
            // THAY ĐỔI 3: Đổi "Relative" (tương đối) thành "Absolute" (tuyệt đối)
            Stream stream = Application.GetResourceStream(new Uri(fullPackUri, UriKind.Absolute)).Stream;
            return new Cursor(stream, true);

        }
    }
}