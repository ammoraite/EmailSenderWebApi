Урок 1. Введение в Razor Pages

1.1 Создайте новый проект Web App (Model-View-Controller)
1.2 Выведите список товаров из каталога на странице catalog/products
1.3 (дополнительно) Добавьте поле с картинкой товара и отобразите товар с картинкой на странице каталога
1.4 Добавьте страницу добавления новых товаров в каталог

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 2. Введение в многопоточность: Thread, Task, ThreadPool, lock

1 Сделайте класс Catalog потокобезопасным
1.1 Создайте собственный потокобезопасный класс ConcurrentList<T>. Чтобы можно было добавлять и удалять 
элементы из разных потоков без ошибок, а также очищать список.

2.1 ★ Постарайтесь сделать свою коллекцию максимально быстрой
2.2 ★★ Реализуйте возможность обхода вашего класса через цикл foreach
2.3 ★★★ Добавьте потокобезопасный метод сортировки элементов в вашей коллекции

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 3. Углубление в многопоточность: ошибки, volatile, TPL, Interlocked и потокобезопасные коллекции

Сделайте класс Catalog потокобезопасным с использованием одной из потокобезопасных коллекций 
из System.Collections.Concurrent. Не забудьте про реализацию метода удаления товара из каталога.

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 4. Понимаем правильно DI, DIP и Service Locator

Реализуйте сервис отправки Email, используя библиотеку MailKit *.
При каждом добавлении нового товара в каталог, отправляйте об этом письмо через созданный сервис.
Не забудьте про DIP и потокобезопасность.
* Для авторизации можете использовать следующие данные:
Сервер: some server
Логин: -
Пароль: -

(Update 13.06) Уточнения:
1. Когда присылаете ДЗ, обязательно указывайте в комментарии почему выбрали тот или иной Lifetime для 
2. сервиса.По возможности используйте синхронный API SmptClient'a, т.к. темы асинхронности мы пока особj
не разбирали и там есть свои особенности, мы обязательно сделаем его асинхронным позже.
3. Помните, что контроллеры должны быть СКРОМНЫМИ (Humble Object), т.е. в них не должно быть бизнес логики.
Размещайте бизнес логику в сервисах - например, в CatalogService.
4. Не надо использовать lock для потокобезопасной отправки писем. Есть как минимум два способа 
обойтись вообще без всяких примитивов синхронизации, просто грамотно применив DI.
При грамотной работе с DI синхронизация вообще редко нужна.

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 5. Конфигурации, логирование, Serilog

1.1 Сделайте класс отправки email’ов настраиваемым через IOptions.
1.2 Логин и пароль от ящика сохраните в пользовательские секреты.
1.3 Корректно подключите Serilog к проекту.
1.4 (дополнительно) Добавьте 3 попытки отправки email’a о добавленном товаре при помощи Polly. Залогируйте
все попытки, обратите внимание на уровни логирования.

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 6. Смысл асинхронности в ASP.NET Core

1 Сделайте сервис отправки писем через EmailKit полностью асинхронным
2 Упражнение: Напишите асинхронный метод, который будет считывать содержимое файлов, имена которых 
переданы в params и возвращать ВСЕ строки этих файлов

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 7. Асинхронность: отмена, ошибки; фоновые сервисы

1.1 Добавьте поддержку отмены в метод добавления товара (пробросьте CancellationToken с самого начала и до конца)
1.2 Каждый час отправляйте себе Email о том, что сервер работает исправно (возможно понадобится ServiceLocator)
2 (дополнительно) Добавьте поддержку асинхронного высвобождения ресурсов в сервис отправки Email’ов

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 8. Обработка ошибок, доменные события

1 Реализуйте регистрацию и перехват доменного события о том, что товар добавлен в каталог. 
Перенесите вызов метода отправки Email’a в специальный обработчик.

Пример реализации доменных событий с вебинара:
https://github.com/rodion-m/GB_ASPNETCourse_202206/tree/master/Lesson.DI/DomainEvents
Статья про доменные события: https://enterprisecraftsmanship.com/posts/domain-events-simple-reliable-solution/

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Урок 9. Middleware

1 Сделайте так, чтобы ваш сайт работал только в браузере Edge. В случае перехода на сайт из другого браузера - напишите, что поддерживается только Edge.
Добавьте логирование тела запросов и логирование тела ответов (используя подход из .net 6).

3.1 * Реализуйте подсчет переходов для всех страниц (HttpContext.Request.Path) (через класс Middleware).
3.2 * И добавьте страницу /metrics с выводом данных о переходах в формате адрес страницы: кол-во переходов.
