#  1. Название проекта: IndoorMap

Платформа для создания графических интерфейсов для промышленных БПЛА;

**BRICS2024: UAS Challenge**, Казань
 
####  Среда разработки:

Язык программирования: **C#**

Фреймворк: **Avalonia UI**

Симуляция: **Gazebo**

#  2. Преимущества

- Предоставляет пользовательский интерфейс для построения блоков для взаимодействия с БПЛА.
- Понижает интеллектуальную планку входа в процесс разработки систем управления БПЛА.
- Предназаначен для пользователей с технической подготовкой в области беспилотных технологий, у которых отсутствует база по направлению разработки программного обеспечения.
 
#  3. Роли участников команды

| ФИО участника | Роль  | Обязанности |
| -------- | ------- |------- |
| Вальчук Александр | Капитан команды; инженер-программист | разработка программного обеспечения |
| Чипурко Андрей | Инженер-техник; Пилот | настройки, отладка и тестирование квадрокоптера |

_Роль 1. Капитан команды_ — организация работы команды в GitHub, осуществление общего руководства работой команды, распределение обязанностей и контроль соблюдения дедлайнов. 

_Роль 2. Инженер-программист_ -- разработка пользовательского интерфейса; написание алгоритмов интерпретации полетных заданий от программы. Работа с визуализацией, написание кода для автономного полета квадрокоптера, разработка алгоритма безопасного полета квадрокоптера.

_Роль 3. Инженер-техник_ — моделирование и изготовление полезной нагрузки квадрокоптера, работа с датчиками, тестирование и техобслуживание и пилотирование квадрокоптера.

_Роль 4. Пилот_ — организация предполетной подготовки, обслуживание БАС, осуществление визуального пилотирования при возникновении внештатных ситуаций.

# 4. Таблица задач
| Описание задачи                                    | Ответственный | Срок выполнения | Статус     | технологии / инструменты / ПО                |
| -------------------------------------------------- | ------------- | --------------- | ---------- | -------------------------------------------- |
| Настройка коптера                                  | А. Чипурко    | 4 часа          | Готово     | Коптер клевер и периферия                    |
| Проверка годности к полету в ручном режиме         | А. Чипурко    | 1 часа          | Готово     | Коптер клевер и периферия                    |
| Проверка годности к полету в автономном режиме     | А. Чипурко    | 1 час           | В процессе | Коптер клевер и периферия                    |
| Постановка задач, декларирование требований        | А. Вальчук    | 2 часа          | Готово     | github / vscode / markdown                   |
| Программа для автономного полета дрона             | А. Вальчук    | 0.5 часа        | Готово     | github / vscode / python                     |
| Каркас/дизайн программного продукта                | А. Вальчук    | 4 часа          | Готово     | github / vscode / dotnet8 / C# / Avalonia UI |
| Модуль построения полетного задания                | А. Вальчук    | 4 часа          | Готово     | github / vscode / dotnet8 / C# / Avalonia UI |
| Модуль отображения графика высот                   | А. Вальчук    | 4 часа          | Назначена  | github / vscode / dotnet8 / C# / Avalonia UI |
| Модуль отображения искуственного горизонта         | А. Вальчук    | 4 часа          | Назначена  | github / vscode / dotnet8 / C# / Avalonia UI |
| Программа выполнения полетного задания на дроне    | А. Вальчук    | 2 часа          | В процессе | github / vscode / python                     |
| Автономные режимы на изменяемых полетных заданиях. | А. Чипурко    | 1 час           | В процессе | Коптер клевер и периферия                    |
