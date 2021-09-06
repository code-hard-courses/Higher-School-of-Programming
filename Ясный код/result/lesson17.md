//используется принудительное время обновления токена(чуть меньше 15 минут), вместо ttl которое отдает сам сервис
- Избыточные комментарии

//если же токен не используется в течение 15 минут, то он протухает не зависимо от ttl
- Избыточные комментарии

//для определение того, что идет логирование вызова actuator, использован MDC который формирует библиотека MSA
- Избыточные комментарии

//на случай если MDC отсутствует, проверяется текст сообщения
- Избыточные комментарии
- Не используйте комментарии там, где можно использовать функцию или переменную

//1-й вызов API - создание новых согласий
- Слишком много информации

//    @Select("with r as (INSERT INTO buildings(created_at, name, address, school_id, rooms_number, floor_count, number, postal_index, county, " +
//            "#{street}, #{gusoevAddressKey}, #{building}, #{description}, #{capacity}, #{unom}, #{isOrgTerritory}, #{image}) returning id) select id from r")
- Закомментированный код

//    Integer persist(Building building);
- Закомментированный код

//    void update(Building building);
- Закомментированный код

//    @RequestMapping(method = RequestMethod.PUT)
//    public WeightedRandom reReadWeigh() {
//        return weightedRandomService.reReadWeigh();
//    }
- Закомментированный код

objectMapper.setTimeZone(TimeZone.getDefault());//to sync dates without timezone
- Шум

private Integer subjectId; // Референс на предмет
- Шум

// update
bellsTimetableAssignmentsRepository.saveOrUpdate(toUpdate);
- Шум

//    public Integer getCount(Map<String, Object> params) {}
- Закомментированный код

// to add basic auth:
- Избыточные комментарии

// int toUpdateBellBeginTime = (int) (toUpdateBell.getBeginTime().getTime() % (24 * 60 * 60 * 1000L));
- Закомментированный код
