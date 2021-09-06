// было
long size = Files.size(path);
// стало
long fileSize = Files.size(path);

// было
User user = userServiceAuthenticator.authenticateByLogin(login, password);
// стало
User authenticatedUser = userServiceAuthenticator.authenticateByLogin(login, password);

// было
certificate = CertificateUtils.getCertificateBase64FromPkcs7(authParameters.getSigned());
// стало
base64Certificate = CertificateUtils.getCertificateBase64FromPkcs7(authParameters.getSigned());

// было
final String generatedDigest = DigestUtils.sha512Hex(serialNumber + deviceId + certificateSalt.getSalt());
final boolean equals = generatedDigest.equals(digest);
log.debug("validate() - end, valid = {}", equals);
return equals
? new ValidationResult(true)
: new ValidationResult(false).setErrorMessage(INVALID_DIGEST).setErrorCode(errorCode());
// стало
final boolean isDigestsEqual = generatedDigest.equals(digest);

// было
final boolean notEmpty = StringUtils.isNotEmpty(authLoginPasswordRequestParams.getPhone());
// стало
final boolean isPhoneNotEmpty = StringUtils.isNotEmpty(authLoginPasswordRequestParams.getPhone());

// было
boolean valid = authCertificateRequestParams.getRefreshToken() == null;
// стало
boolean isTokenValid = authCertificateRequestParams.getRefreshToken() == null;

// было
final AdditionalAuthInfo info = additionalAuthService.getAdditionalAuthInfoByUserId(uid);
// стало
final AdditionalAuthInfo additionalAuthInfo = additionalAuthService.getAdditionalAuthInfoByUserId(uid);

// было
PersonalDataChangeEntry entry = Optional.ofNullable(
personalDataChangeRepository.findOne(ValidityArtifactPk.of(userId, VERIFY_EMAIL))
).orElseThrow(() -> new IllegalArgumentException("Email change was not requested"));
// стало
PersonalDataChangeEntry personalDataChangeEntry = Optional.ofNullable(
personalDataChangeRepository.findOne(ValidityArtifactPk.of(userId, VERIFY_EMAIL))
).orElseThrow(() -> new IllegalArgumentException("Email change was not requested"));

// было
ExternalUserDto user = getUserById(userId)
.orElseThrow(() -> new IllegalArgumentException("Cannot get personal data"));
// стало
ExternalUserDto externalUserDto = getUserById(userId)
.orElseThrow(() -> new IllegalArgumentException("Cannot get personal data"));

// было
Optional<PersonalDataChangeEntry> entry = personalDataChangeRepository
.findByPkAndDateChangedIsNotNull(ValidityArtifactPk.of(userId, CHANGE_PHONE));
// стало
Optional<PersonalDataChangeEntry> phoneChangeEntry = personalDataChangeRepository
.findByPkAndDateChangedIsNotNull(ValidityArtifactPk.of(userId, CHANGE_PHONE));

// было
public static String maskEmail(String email) {
String res = Optional.ofNullable(email)
.map(value -> value.replaceAll(EMAIL_MASK_REGEX, MASK))
.orElse(null);

    log.debug("maskEmail() - end: {}", res);
    return res;
}
// стало
public static String maskEmail(String email) {
String maskedEmail = Optional.ofNullable(email)
.map(value -> value.replaceAll(EMAIL_MASK_REGEX, MASK))
.orElse(null);

    log.debug("maskEmail() - end: {}", maskedEmail);
    return maskedEmail;
}

// было
List<DirContextOperations> list = ldapTemplate.search(query, (ContextMapper) o -> (DirContextOperations) o);
// стало
List<DirContextOperations> dirContextOperationsList = ldapTemplate.search(query, (ContextMapper) o -> (DirContextOperations) 