### Общий F.A.Q:
- Шифруемые сообщения и ключи **ВСЕГДА** вводятся **ЗАГЛАВНЫМИ** буквами и имеют соответствующие коды для каждого алгоритма (см. таблицы ниже);

### DES и ГОСТ 28147-89
- В соответствие с методичкой, используется алфавит из **32** букв (без Ё);
- **ПРОБЕЛ** имеет другой код (в методичке неверный)

|  Буква |DEX   |HEX   | Буква| DEX  |HEX|
| :-----------: | :------------: | :------------: | :------------: | :------------: | :------------: |
| А  |  192 | 11000000  |С |  209 | 11010001  |
|   Б|   193|  11000001 | Т|  210 |  11010010 |
|   В|  194 |  11000010 | У|  211 |  11010011 |
|   Г|  195 |  11000011 | Ф|  212 |  11010100 |
|  Д |  196 |  11000100 | Х|  213 | 11010101 |
| Е  |   197|  11000101 | Ц|  214 | 11010110  |
|   Ж|  198 |  11000110 | Ч| 215 |  11010111 |
|   З|   199|   11000111| Ш| 216  | 11011000  |
|   И|  200 | 11001000  | Щ| 217  |  11011001 |
|   Й|   201| 11001001  | Ъ| 218  | 11011010  |
|   К|   202|  11001010 | Ы|219  |   11011011|
|   Л|   203|  11001011 | Ь|   220| 11011100  |
|   М|   204|  11001100 | Э|   221|  11011101 |
|   Н|  205 | 11001101 | Ю|   222| 11011110  |
|   О|   206|  11001110 | Я|   223|   11011111|
|   П|  207 |  11001111 | **ПРОБЕЛ**| 32|  **00100000**|
| Р| 208| 11010000 |

### RSA, функция хеширования и ЭЦП
- В соответствие с методичкой, используется алфавит из **33** букв (см. таблицу ниже):

|  Буква |DEX   | Буква| DEX  |
| :------------: | :------------: | :------------: | :------------: |
| А  |  1 | Р |  18 |
|   Б|   2|  С|  19 |
|   В|  3 |   Т|  20 |
|   Г|  4 |   У|  21 |
|  Д |  5 |   Ф|  22 |
| Е  |   6|   Х|  23 |
|   **Ё**|  **7** |   Ц| 24 |
|   Ж|   8|  Ч| 25  |
|   З|  9 |Ш| 26  |
|   И|   10| Щ| 27  |
|   Й|   11|   Ъ|28  |
|   К|   12|   Ы|   29|
|   Л|   13|   Ь|   30|
|   М|  14 |  Э|   31|
|   Н|   15|   Ю|   32|
|   О|  16 |   Я| 33|
| П| 17|
