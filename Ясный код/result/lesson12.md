private String buildRepoUri() {
String user = GitProps.getUsername();
String password = GitProps.getPassword();
return "https://" + user + ":" + password + "@repo.mos.ru/" + repoName + ".git";
}
//  user и password вынесены в отдельный метод для минимизации видимости, сгруппированы связанные команды

private String buildSourcePath() {
String root = gitLocalDir.getAbsolutePath() + "/";
String path = root + "src/";
return Files.exists(Paths.get(path)) ? path : root;
}
//  root и path вынесены в отдельный метод для минимизации видимости, сгруппированы связанные команды

protected boolean isNewChangeType(DiffEntry diff) {
DiffEntry.ChangeType changeType = diff.getChangeType();
return changeType == DiffEntry.ChangeType.ADD
|| changeType == DiffEntry.ChangeType.COPY
|| changeType == DiffEntry.ChangeType.RENAME;
}
// минимизирована видимость changeType, сгруппированы связанные команды

private void checkUserIdFromOtpVerification(String userId, String responseUserId) {
boolean idsEqual = Objects.equals(userId, responseUserId);
if (!idsEqual) {
throw new PersonalAreaException(ErrorConstants.ERROR_CODE_DBO_INVALID_CREDENTIALS, INVALID_PASS);
}
}
// сгруппированы команды для улучшения читаемости и ограничения видимости idsEqual

private boolean isDataVerified(ExternalUserDto user,
PersonalDataChangeEntry changeEntry,
Function<ExternalUserDto, String> extractor) {
String valueFromLdap = extractor.apply(user);
String value = changeEntry.getValue();

    if (value == null) {
        return true;
    }

    return isNotBlank(valueFromLdap) && valueFromLdap.equals(value);
}
// сгруппированы команды, ограничена видимость переменных

private boolean isEmailVerificationTimeoutPassed(Optional<PersonalDataChangeEntry> emailChangeEntry) {
return emailChangeEntry.map(PersonalDataChangeEntry::getDateChanged)
.map(dateChanged -> {
long timeSinceEmailChanged = new Date().getTime() - dateChanged.getTime();
log.trace("getPersonalData(), email was changed {} ms ago", timeSinceEmailChanged);
return timeSinceEmailChanged
> personalAreaProperties.getChangeEmailTimeout() * DateUtils.MILLIS_PER_SECOND;
}).orElse(Boolean.FALSE);
}
// сгруппированы команды, улучшена читаемость кода

private void saveBlockJournalIfNeeded(Boolean wasBlockedBefore, String value, String attribute) {
final String uid = repository.findUidByParams(value, attribute);
if (!wasBlockedBefore && blockStrategy.isBlocked(uid)) {
saveBlockJournal(uid, BlockType.BLOCK_AUTO,
localizedErrorMessagesProvider.getLocalizedMessage(
ACCOUNT_LOCKED_REASON,
LocalizedErrorMessagesProvider.LocaleName.RUSSIAN.getDisplayValue()
)
);
}
}
// вынесен в отдельный метод функционал сторонней операции

private @Nullable User findUserByAttributeParams(String value, String attribute) {
String uidByParams = repository.findUidByParams(value, attribute);
if (uidByParams == null) {
return null;
}
return repository.getUser(uidByParams);
}
// ограничена видимость промежуточной переменной, сгруппированы связанные команды

private User searchUser(LdapQuery query) {
List<DirContextOperations> list = ldapTemplate.search(query, (ContextMapper) o -> (DirContextOperations) o);
return list.isEmpty() ? null : fillUser(list.get(0));
}
// улучшена читаемость

private User fillUser(DirContextOperations ctx) {
User user = new User();
user.setUsername(ctx.getStringAttribute(ldapFields.getFieldName()));
user.setUserInfo(fillUserInfo(ctx));
user.setEncryptionType(ctx.getStringAttribute(ldapFields.getEncryptionType()));
user.setSalt(ctx.getStringAttribute(ldapFields.getSalt()));
user.setExternalKey(ctx.getStringAttribute(ldapFields.getFieldExternalKey()));
return user;
}
// ограничена видимость user, улучшена читаемость

private UserInfo fillUserInfo(DirContextOperations ctx) {
UserInfo userInfo = new UserInfo();
userInfo.setId(ctx.getStringAttribute(ldapFields.getFieldUid()));
userInfo.setName(ctx.getStringAttribute(ldapFields.getFieldName()));
userInfo.setStatus(ctx.getStringAttribute(ldapFields.getStatus()));
userInfo.setSmsActivationRequired(Boolean.parseBoolean(ctx.getStringAttribute(ldapFields.getIsSmsRequired())));
userInfo.setSecurityProfile(getUserPasswordPolicy(userInfo.getId()));
userInfo.setSessionDuration(userInfo.getSecurityProfile().getSessionDuration());
userInfo.setLogin(ctx.getStringAttribute(ldapFields.getFieldLogin()));
userInfo.setPhoneNumber(ctx.getStringAttribute(ldapFields.getPhoneNumber()));
final String lockTimeAttr = ctx.getStringAttribute(ldapFields.getDateTimeOfAutoBlocking());
userInfo.setBlocked(StringUtils.isNotEmpty(lockTimeAttr));
String lastModified = ctx.getStringAttribute(ldapFields.getPasswordChangedDate());
if (StringUtils.isNotBlank(lastModified)) {
userInfo.setLastModified(DateUtils.convertToDate(lastModified));
}
return userInfo;
}
// ограничена видимость user, улучшена читаемость

private void checkPasswordPolicyExists(DirContextOperations ctx) {
String uid = ctx.getStringAttribute(ldapFields.getFieldUid());

    NameAwareAttributes passwordPolicyAttribute = ldapCommonRepository.getUserAttribute(uid, ldapFields.getSecurityProfileDn());

    if (passwordPolicyAttribute == null || passwordPolicyAttribute.size() == 0) {
        throw new AuthException(ERROR_CODE_DBO_ACCESS_DENIED, ERROR_CODE_DBO_ACCESS_DENIED_DSCRN);
    }
}
// вынесен в отдельный метод функционал, ограничена видимость passwordPolicyAttribute

private SignatureInstrument getSignatureInstrumentByCertificatePath(String certificatePath) {
LdapQuery query = LdapQueryBuilder.query()
.base(pathSignature + COMMA + baseDn)
.filter(new EqualsFilter(ldapFields.getFieldCertificateObjectClass(), certificatePath));
DirContextOperations ctx = ldapTemplate.searchForContext(query);
return fillDigestSignature(ctx);
}
// улучшена читаемость

public void checkDigestSignature(SignatureInstrument signatureInstrument) {
if (!signatureInstrument.isEnableAuth()
|| signatureInstrument.getStatus() != SignatureInstrumentStatus.ACTIVE
) {
log.debug("attribute value is not valid. Digest signature is enable authentication: {}, status: {}",
signatureInstrument.isEnableAuth(), signatureInstrument.getStatus());
throw new AuthenticationException();
}
}
// вынесен в отдельный метод функционал сторонней операции

private String getPswdHash(LdapQuery query) {
List<DirContextOperations> list = ldapTemplate.search(query, (ContextMapper) o -> (DirContextOperations) o);
return list.isEmpty() ? StringUtils.EMPTY : getUserPswdFromContext(list.get(0));
}
// вынесен в отдельный метод функционал сторонней операции
