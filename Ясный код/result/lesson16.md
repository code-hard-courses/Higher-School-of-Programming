// Категорично использую комментарии только в тестах given/when/then.
// Их можно отнести к информативным комментариям.
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


// Также обязательно применяю JavaDoc, но это не комментарии.

// Представление намерений
Считаю, что код должен отражать намерения программиста, если это не так, стоит переписать код.

// Прояснение
Верный признак грязного кода - переписать.

// Предупреждения о последствиях
В Java для этого введены Exceptions, всё остальное - грязный код.

// Усиление
Мне видится пункт надуманным, опять-таки, если есть опасения, значит код не покрыт тестами
и/или код не отражает намерения его создателей = грязный код.

// Комментарии TODO
В Kotlin, например, для этого есть исключение NotImplementedError.
