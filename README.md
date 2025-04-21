# CSV1
// 原始尚未分割   讀取    讀+寫          
// 一萬筆資料     26ms    1902ms
// 十萬筆資料     313ms   16487ms
// 百萬筆資料     2801ms  178555ms
// 千萬筆資料     39202ms 3056861ms

// 一萬筆分割    讀取     讀+寫
// 十萬筆資料    359ms    873ms
// 百萬筆資料    3071ms   7584ms
// 千萬筆資料    32725ms  77044ms

// 一萬筆平行    讀取     讀+寫
// 十萬筆資料    362ms    899ms
// 百萬筆資料    1491ms   3257ms
// 千萬筆資料    21302ms  36571ms

註:
if (!Directory.Exists(filepathfinal))
{
    Directory.CreateDirectory(filepathfinal);
}
尋找路徑是否存在很耗記憶體效能，因此不要寫在迴圈裡，只要在一開始做一次性檢查即可
