using UnityEngine;

namespace util
{
    public class ParamsGetter
    {
        private static ParamsGetter instance = new ParamsGetter();
        private int _unit = 8;

        public static ParamsGetter GetInstance() {
            return instance;
        }

        public int GetUnit() { return _unit; }

        public void SetUnit(int unit) { _unit = unit; }

        //  五线谱总高度（从上加三线到下加三线）
        public int GetTotalHeight() { return 10 * _unit; }

        //  五线谱高度（不包括上加及下加部分）
        public int GetStaffHeight() { return 4 * _unit; }

        //  五线谱实际位置
        public int GetStaffPosition() { return 11 * _unit / 2; }

        //  五线谱中心位置
        public int GetStaffCenterPosition() { return GetTotalHeight() / 2; }

        //  五线谱头部信息宽度
        public int GetHeadWidth() { return 6 * _unit; }

        //  五线谱拍号信息宽度
        public int GetBeatWidth() { return 2 * _unit; }

        //  五线谱段落宽度
//        public int GetParagraphWidth(Activity activity) {
//            return activity.getResources().getDisplayMetrics().widthPixels - 8 * _unit;
//        }

        //  用于画符号的Paint的字体大小
        public int GetSymbolSize() { return 4 * _unit; }

        // 音高位置差
        public int GetPitchPositionDiff() { return _unit / 2; }

        //  符号视图起始
        public int GetSymbolStart() { return _unit; }

        //  音符符头宽度
        public int GetNoteHeadWidth() { return _unit; }

        //  音符符干长度
        public int GetNoteStemHeight() { return 7 * _unit / 2; }

        // 音符符干朝下时距离乐符中心的偏移
        public int GetNoteStemDownShift() { return _unit / 8; }

        // 音符符尾朝上时的中心纵向偏移
        public int GetNoteTailUpPortraitShift() { return 3 * _unit / 2; }

        // 音符符尾朝下时的中心纵向偏移
        public int GetNoteTailDownPortraitShift() { return 3 * _unit / 2; }

        // 音符符尾朝上时的中心横向偏移
        public int GetNoteTailUpLandscapeShift() { return _unit / 8; }

        // 音符符尾朝下时的中心横向偏移
        public int GetNoteTailDownLandscapeShift() { return _unit / 8; }

        // 音符最右边相对于乐符中心的横坐标偏移量
        public int GetNoteRightShift() { return _unit / 2; }

        // 音符最左边相对于乐符中心的横坐标偏移量
        public int GetNoteLeftShift() { return 3 * _unit / 4 ; }

        //  音符符杠间距
        public int GetNoteBeamDiff(bool upOrDown) {
            if (upOrDown)
            {
                return 3 * _unit / 4;
            }
            else
            {
                return -3 * _unit / 4;
            }
        }

        //  附点位置
        public int GetDotePosition() { return GetNoteHeadWidth() + _unit / 2; }

        //  第一个升降号位置
        public float GetFirstFifthsPosition() { return (float) (3.6 * _unit); }

        //  第二个升降号位置
        public float GetSecondFifthsPosition() { return (float) (4.6 * _unit); }

        // 谱号离最左边位置
        public int GetClefPortraitShift() { return 3 * _unit / 2; }

        // 节拍离最左边位置
        public int GetBeatPortraitShift() { return 5 * _unit; }


        // 乐谱字体，从资源resources文件夹中读取
        public Font GetSymbolFont()
        {
            Font scoreFont = (Font) Resources.Load("Fonts/mscore-20");
            return scoreFont;
        }
    }
}
