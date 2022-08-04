3.1 Прокомментировать
@Test
fun `should authorise new customer in  service`() {
// given
val email = "test@domain.com"

    // when
    mockMvc.perform(get(url)
        .contentType(MediaType.APPLICATION_JSON)
        .characterEncoding("UTF-8")
        .andDo(print())
        .andExpect(status().is2xxSuccessful)
        .andExpect(content().contentType(MediaType.APPLICATION_JSON))
        .andExpect(jsonPath("$.token").value(token))

    // then
    assertThat(profile)
        .hasFieldOrPropertyWithValue("email", email)
}

// Привожу только этот пример ибо категорично использую комментарии только в тестах таким образом.
// Также обязательно применяю JavaDoc, но это не комментарии.

3.2 Убрать комментарии
//используется принудительное время обновления токена(чуть меньше 15 минут), вместо ttl которое отдает сам сервис

//если же токен не используется в течение 15 минут, то он протухает не зависимо от ttl

//для определение того, что идет логирование вызова actuator, использован MDC который формирует библиотека MSA

//на случай если MDC отсутствует, проверяется текст сообщения

//1-й вызов API - создание новых согласий
