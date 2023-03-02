<?php
$hour = 7;
$min = 30;


interface TimeToWordConvertInterface{
    public function convert($hours,$minutes):string;
}

class TimeToWordConvert implements TimeToWordConvertInterface{

private $massMin;
private $massHours;
private $massHoursFull;
private $massHoursQua;
private $plusHour;
private $minutes2;
private $res;

public function convert($hours,$minutes):string{
$massMin[1] = "Одна минута";
$massMin[2] = "Две минуты";
$massMin[3] = "Три минуты";
$massMin[4] = "Четыре минут";
$massMin[5] = "Пять минут";
$massMin[6] = "Шесть минут";
$massMin[7] = "Семь минут";
$massMin[8] = "Восемь минут";
$massMin[9] = "Девять минут";
$massMin[10] = "Десять минут";
$massMin[11] = "Однадцать минут";
$massMin[12] = "Двенадцать минут";
$massMin[13] = "Тринадцать минут";
$massMin[14] = "Четырнадцать минут";
$massMin[15] = "Четверть";
$massMin[16] = "Шестнадцать минут";
$massMin[17] = "Семнадцать минут";
$massMin[18] = "Восемнадцать минут";
$massMin[19] = "Девятнадцать минут";
$massMin[20] = "Двадцать минут";
$massMin[21] = "Двадцать одна минута";
$massMin[22] = "Двадцать две минуты";
$massMin[23] = "Двадцать три минуты";
$massMin[24] = "Двадцать четыре минут";
$massMin[25] = "Двадцать пять минут";
$massMin[26] = "Двадцать шесть минут";
$massMin[27] = "Двадцать семь минут";
$massMin[28] = "Двадцать восемь минут";
$massMin[29] = "Двадцать девять минут";
$massMin[30] = "Половина";

$massHours[1] = "часа";
$massHours[2] = "двух";
$massHours[3] = "трёх";
$massHours[4] = "четырёх";
$massHours[5] = "пяти";
$massHours[6] = "шести";
$massHours[7] = "семи";
$massHours[8] = "восьми";
$massHours[9] = "девяти";
$massHours[10] = "десяти";
$massHours[11] = "одинадцати";
$massHours[12] = "двенадцати";

$massHoursFull[1] = "Один час";
$massHoursFull[2] = "Два часа";
$massHoursFull[3] = "Три часа";
$massHoursFull[4] = "Четыре часа";
$massHoursFull[5] = "Пять часов";
$massHoursFull[6] = "Шесть часов";
$massHoursFull[7] = "Семь часов";
$massHoursFull[8] = "Восемь часов";
$massHoursFull[9] = "Девять часов";
$massHoursFull[10] = "Десять часов";
$massHoursFull[11] = "Одинадцать часов";
$massHoursFull[12] = "Двенадцать часов";

$massHoursQua[1] = "первого";
$massHoursQua[2] = "второго";
$massHoursQua[3] = "третьего";
$massHoursQua[4] = "четвёртого";
$massHoursQua[5] = "пятого";
$massHoursQua[6] = "шестого";
$massHoursQua[7] = "седьмого";
$massHoursQua[8] = "восьмого";
$massHoursQua[9] = "девятого";
$massHoursQua[10] = "десятого";
$massHoursQua[11] = "одиннадцатого";
$massHoursQua[12] = "двунадцатого";

$plusHour = $hours + 1;
if ($plusHour == 13):
    $plusHour = 1;
endif;

if ($minutes == 0):
    $res = "$massHoursFull[$hours]";
elseif ($minutes <= 30 && $minutes > 0):
    if ($minutes == 15):
        $res = "Четверть $massHoursQua[$plusHour]";
    elseif ($minutes == 30):
        $res = "Половина $massHoursQua[$plusHour]";
    else:
        $res = "$massMin[$minutes] после $massHours[$hours]";
    endif;
else:
    if ($minutes == 58):
        $res = "Две минуты до $massHours[$plusHour]";
    elseif ($minutes == 59):
        $res = "Одна минута до $massHours[$plusHour]";
    elseif ($minutes == 45):
        $res = "Пятнадцать минут до $massHours[$plusHour]";
    else:
        $minutes2 = 60 - $minutes;
        $res = "$massMin[$minutes2] до $massHours[$plusHour]";
    endif;
endif;

if ($minutes < 10):
    $res = "$hours:0$minutes - $res";
else:
    $res = "$hours:$minutes - $res";
endif;

return $res;}
}

$objTime = new TimeToWordConvert;
echo $objTime->convert($hour,$min);



?>
