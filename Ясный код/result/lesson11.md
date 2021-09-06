public T removeFront() {
if (size == 0) {
return null;
}

    Node<T> toRemove = head.next;
    T value = toRemove.value;

    head.next = toRemove.next;
    toRemove.next.prev = head;

    toRemove.next = null;
    toRemove.prev = null;

    size--;
    return value;
}
// очищаем ссылки переменной toRemove, чтобы GC мог её удалить

Multimap<Commit, UnhandledUnit> unhandled = HashMultimap.create();
Multimap<Commit, AffectedUnit> affected = HashMultimap.create();
for (Commit commit : commits) {
List<UnhandledUnit> unhandledList = new ArrayList<>();
List<AffectedUnit> affectedList = new ArrayList<>();

    commit.getDiffs().forEach(diff -> diffHandlerChain.handle(diff, affectedList, unhandledList));

    unhandled.putAll(commit, unhandledList);
    affected.putAll(commit, affectedList);
}
// инициализация unhandled и affected перенесена из начала метода к моменту их применения

StringBuilder result = new StringBuilder();
if (fixedPhone.length() < PHONE_LENGTH) {
result.append(PHONE_START_WITH_7).append(fixedPhone);
} else if (fixedPhone.length() == PHONE_LENGTH) {
if (!fixedPhone.startsWith(PHONE_START_WITH_7)) {
result.append(PHONE_START_WITH_7).append(fixedPhone.substring(1));
} else {
result.append(fixedPhone);
}
} else {
result.append(fixedPhone);
}
// result перенесен к месту первого применения

String body = null;
if (feignException.responseBody().isPresent()) {
body = feignException.contentUTF8();
}
// body перенесен к месту первого применения

final UUID id = userService.getUserData(unc).getId();
try {
auditLogService.logAcceptAgreement(unc, id, EventClass.SUCCESS);
} catch (Exception e) {
auditLogService.logAcceptAgreement(unc, id, EventClass.FAILURE);
throw e;
}
// id перенесен к месту первого применения, сделан final

final Map<String, Object> tokenPayload = new HashMap<>();
try {
tokenPayload.putAll(super.extractTokenPayload());
tokenPayload.put(CHANNEL, tokenPayload.get(SP_NAME));
} catch (Exception e) {
tokenPayload.put(SUB, request.getHeader(UNC));
tokenPayload.put(CHANNEL, "");
}
// tokenPayload перенесен к месту первого применения, сделан final

ValidationResult result;
try {
result = settingsClient.validateAddress(validationAddressData);
} catch (Exception e) {
log.error(e.getLocalizedMessage());
result = new ValidationResult();
result.setMessage(e.getLocalizedMessage());
}
// result перенесен к месту первого применения


long mdmId = -1;
try {
RelativeCrossReferenceId req = new RelativeCrossReferenceId(MdmMapper.SYSTEM_NUMBER, unc.toString());
CompletableFuture<UserCrossReferencesDTO> response = mdmClient.getPersonCrossReferences(req);
UserCrossReferencesDTO crossReferenceResponse = response.get();
mdmId = crossReferenceResponse.getPartyUId();
} catch (Exception e) {
log.error(e.getLocalizedMessage());
}
// mdmId перенесен к месту первого применения, инициализирован, использован примитив вместо обертки


final Map<String, Object> additionalInformation = oAuth2AccessToken.getAdditionalInformation();
final String jti = (String) additionalInformation.get(JwtAccessTokenConverter.TOKEN_ID);
final String sessionId = (String) additionalInformation.get(Constants.KEY_SESSION_ID);
final String authHeader = buildAuthorizationHeader(oAuth2AccessToken);
final Date expirationDate = userExpirationProvider.getExpirationDateByDurationInSeconds(userInfo.getSessionDuration());
sessionStorageService.save(
new SessionInfo(
sessionId,
jti,
certificateSN,
expirationDate,
signId,
!userInfo.isSmsActivationRequired(),
Collections.emptyList()),
authHeader
);
// выделены переменные для повышения читаемости кода, сделаны final

if (session == null) {
throw new AuthException(ErrorConstants.ERROR_CODE_DBO_EXPIRED_SESSION, ErrorConstants.EXPIRED_SESSION, HttpStatus.SC_UNAUTHORIZED);
}
// выполнена проверка на инвариант

if (StringUtils.isEmpty(cert)) {
throw new AuthException(ErrorConstants.ERROR_CODE_DBO_0006, ErrorConstants.INVALID_CERTIFICATE);
}
// выполнена проверка на инвариант

if (authenticatedUser == null) {
throw new AuthException(ErrorConstants.CERTIFICATE_NOT_ASSOCIATED_WITH_USER, StringUtils.EMPTY);
}
// выполнена проверка на инвариант

BlockStrategy lockStrategy = blockStrategyFactory.build(passwordPolicy);
if (blockState == BlockState.UNBLOCKED) {
try {
ctx = ldapCommonRepository.authenticateUser(uid, query, password);

        if (userWasBlocked) {
            lockStrategy.resetBlockStateWithAudit(ctx, uid, commonName);
        } else {
            lockStrategy.resetBlockState(ctx, uid);
        }
    } catch (AuthenticationException ex) {
        if (isUserPasswordExpired(uid, passwordPolicy)) {
            throw new PasswordExpiredException(ERROR_CODE_DBO_EXPIRED_PSWD, ex);
        }
        lockStrategy.block(uid, passwordPolicy);
        if (lockStrategy.isBlocked(uid)) {
            throw ex;
        }

        ctx = ldapCommonRepository.authenticateUser(uid, query, password);
        lockStrategy.resetBlockStateWithAudit(ctx, uid, commonName);
    }
}
// lockStrategy перенесен к месту первого применения

long min = (long) Math.pow(10, (double) length - 1);
long max = 10 * min;
String res = String.valueOf(min + (long)(NUMBER_GENERATOR.nextDouble() * (max - min)));
// выделены min и max для повышения читаемости кода

PersonalDataDto personalDataDto = new PersonalDataDto();
if (!Objects.equals(emailFromLdap, container.getEmail())) {}
// выделена и инициализирована переменная для повышения читаемости кода
