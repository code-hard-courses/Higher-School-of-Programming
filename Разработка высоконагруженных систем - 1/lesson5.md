## 5. Сложность борьбы со сбоями

Фатальные сбои винчестеров, дефекты ОЗУ, отключение электропитания, неверное переключение сетевого кабеля… В больших ЦОДах (центрах обработки и хранения данных) подобное происходит постоянно из-за наличия большого количества машин. Например, среднее время наработки на отказ жёстких дисков составляет несколько десятков лет. Таким образом, в кластере с 10 тысячами винчестеров следует ожидать в среднем одного отказа жёсткого диска в сутки.

Естественная реакция --  **повысить избыточность аппаратного обеспечения** . Можно создать RAID-массивы из дисков, обеспечить дублирование электропитания серверов и наличие в них CPU с возможностью горячей замены, а также запастись аккумуляторными батареями и дизельными генераторами в качестве резервных источников электропитания ЦОДов. При отказе одного компонента его место на время замены занимает резервный компонент. Такой подход полностью не предотвращает отказы, связанные с проблемами оборудования, но в целом вполне приемлем и часто способен поддерживать бесперебойную работу машин в течение многих лет.

Ещё буквально в 2010-х годах избыточность аппаратных компонентов была достаточной для большинства систем, и критический отказ отдельной машины становился редким явлением. Программные сервисы достаточно быстрого восстановления из резервной копии на новой машине делали время простоя не катастрофичным.

Однако по мере роста объёмов данных и вычислительных запросов всё больше прикладных систем пользователей начали использовать большее количество машин, что естественно привело к пропорциональному росту частоты отказов оборудования. Более того, в крупнейших облачных сервисах наподобие Amazon виртуальные машины достаточно часто становятся недоступными без предупреждения, поскольку такие платформы отдают предпочтение гибкости и способности быстро адаптироваться всей системы в целом в ущерб надёжности одной машины.

Поэтому сегодня происходит сдвиг в направлении **систем, способных перенести полное отключение отдельных машин целиком** -- благодаря применению методов устойчивости к сбоям, взамен избыточности аппаратного обеспечения или дополнительно к ней. Такая устойчивая к аппаратным сбоям система допускает например установку исправлений по отдельным узлам в горячем режиме, без вынужденного бездействия всей системы.

В случае приложений на стороне сервера может понадобиться выполнить **плавающее обновление** (rolling upgrade), также называемое поэтапным развертыванием (staged rollout). При нём новая версия развертывается всего на нескольких узлах за раз и проверяется, работает ли она без проблем; так постепенно обходятся все узлы. Благодаря этому новые версии развертываются без простоя сервиса.

В случае же приложений на стороне клиента всё зависит от подготовленности пользователей, которые могут решить достаточно долго не устанавливать обновления. Это решается, например, принудительным сообщением на клиенте о необходимости установки обновления, которое проверяется клиентским ПО в момент запуска и регистрации на сервере.

Другой класс сбоев --  **систематическая программная ошибка в системе** . Она обычно вызывает гораздо больше системных отказов, чем некоррелируемые аппаратные сбои. Примеров тому множество каждый год – в 2020-м не раз отказывали и амазон, и google, и телеграм...

Программная ошибка, приводящая к фатальному сбою экземпляра сервера, нередко случается по прозаическим причинам. Например, выходит из-под контроля процесс, полностью исчерпавший какой-либо общий ресурс: мощность процессора, оперативную память, пространство на диске или полосу пропускания сети. Сервис, от которого зависит работа системы, замедляется, перестает отвечать на запросы или начинает возвращать испорченные ответы. В итоге формируются каскадные сбои, когда крошечный сбой в одном компоненте вызывает сбой в другом компоненте, а тот, в свою очередь, вызывает дальнейшие сбои. И такие каскады нередко выходят за пределы одной системы, «вырубая» множество серверов, завязанных на данную систему.

Причём ошибки, вызывающие подобные программные сбои, часто долго остаются неактивными, вплоть до первого в истории момента срабатывания под влиянием необычных обстоятельств.

Быстрого решения проблемы систематических ошибок в программном обеспечении не существует. Наиболее перспективным следует считать всевозможные методы контроля формальной корректности кода и системы, но пока они слишком сложны и дороги.

Надёжность потенциально устойчивых к сбоям систем имеет смысл проверять через искусственное повышение частоты сбоев с помощью их умышленной генерации -- например, путем прерывания работы отдельных, выбранных случайным образом процессов без предупреждения. Многие критические ошибки на этапе эксплуатации фактически случаются из-за плохо организованной обработки ошибок в системе. Умышленное порождение сбоев гарантирует постоянное тестирование механизмов обеспечения устойчивости к ним и обычно косвенно влияет и на выявление ранее неведомых ошибок.


## Сложность борьбы со сбоями (задания)

======= 14. Какой тип сбоев самый опасный?

[ ] аппаратные

[ 1] программные

[ ] плавающие

[ ] человеческие

======= 15. Что сегодня наиболее перспективно в плане борьбы с проблемой систематических ошибок?

[ ] аппаратная избыточность

[ ] программная надёжность

[1 ] устойчивость к сбоям

======= 16. Как эффективнее повышать надёжность системы?

[ ] попытками понижения частоты сбоев

[ 1] искусственным повышением частоты сбоев

[ ] плавающими обновлениями

[ ] тестированием
