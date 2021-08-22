# 郵便番号CSVファイル(KEN_ALL.CSV) Parser

[![build](https://github.com/natsutteatsuiyone/KenAllCsv/actions/workflows/ci.yml/badge.svg)](https://github.com/natsutteatsuiyone/KenAllCsv/actions/workflows/ci.yml)

* 複数行に分割されている住所をマージ
* 以下に掲載がない場合、（全域）等の付加情報を削除
* ビルの階数表記から丸括弧を削除
* 地割、丸括弧内の丁目、番地等を削除
* 丸括弧内の地名を複数行に分割