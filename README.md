# 郵便番号CSVファイル(KEN_ALL.CSV) Parser

[![build](https://github.com/natsutteatsuiyone/KenAllCsv/actions/workflows/ci.yml/badge.svg)](https://github.com/natsutteatsuiyone/KenAllCsv/actions/workflows/ci.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/9dc898ea0bfb411fa7adcb79fa67903a)](https://www.codacy.com/gh/natsutteatsuiyone/KenAllCsv/dashboard?utm_source=github.com&utm_medium=referral&utm_content=natsutteatsuiyone/KenAllCsv&utm_campaign=Badge_Grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/9dc898ea0bfb411fa7adcb79fa67903a)](https://www.codacy.com/gh/natsutteatsuiyone/KenAllCsv/dashboard?utm_source=github.com&utm_medium=referral&utm_content=natsutteatsuiyone/KenAllCsv&utm_campaign=Badge_Coverage)

-   複数行に分割されている住所をマージ
-   以下に掲載がない場合、（全域）等の付加情報を削除
-   ビルの階数表記から丸括弧を削除
-   地割、丸括弧内の丁目、番地等を削除
-   丸括弧内の地名を複数行に分割
