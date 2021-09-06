// было
public class FraudEventHelper {
public void sendFraudEvent(String eventType, User user) {
UserInfo userInfo = user != null ? user.getUserInfo() : null;
userInfo = loginCryptoUtil.encryptLogin(userInfo);
sendFraudEvent(eventType, userInfo);
}
}
// стало
public class FraudEventHelper {
public void sendWithUser(String eventType, User user) {
UserInfo userInfo = user != null ? user.getUserInfo() : null;
userInfo = loginCryptoUtil.encryptLogin(userInfo);
sendFraudEvent(eventType, userInfo);
}
}
// комментарий
- имя метода содержало имя объекта класса
- добавлено указание на специфику данного метода для пользователя

// было
public class UuidSaltProvider implements SaltProvider {
public Long generateSalt() {
return UUID.randomUUID().getLeastSignificantBits();
}
}
// стало
public class UuidSaltProvider implements SaltProvider {
public Long provide() {
return UUID.randomUUID().getLeastSignificantBits();
}
}
// комментарий
- имя метода не отвечало намерениям класса

// было
public abstract class LdapUserRepository implements UserRepository {
public User getUser(String uid) {
LdapQuery query = LdapQueryBuilder.query().attributes(ldapFields.getUserLdapAttributes()).base(pathClient
+ COMMA + baseDn).filter(new EqualsFilter(ldapFields.getFieldUid(), uid));
return searchUser(query);
}
}
// стало
public abstract class LdapUserRepository implements UserRepository {
public User getByUid(String uid) {
LdapQuery query = LdapQueryBuilder.query().attributes(ldapFields.getUserLdapAttributes()).base(pathClient
+ COMMA + baseDn).filter(new EqualsFilter(ldapFields.getFieldUid(), uid));
return searchUser(query);
}
}
// комментарий
- имя метода содержало имя объекта класса
- имя расширено с учетом специфики выполняемой операции

// было
public class AdditionalAuthService {
public void saveAdditionalAuthInfo(String uid) {
final AdditionalAuthInfo additionalAuthInfo = new AdditionalAuthInfo()
.setUserId(uid);
additionalAuthInfoRepository.save(additionalAuthInfo);
}

    public void deleteAdditionalAuthInfo(String uid) {
        log.debug("delete additional auth info entry for user uid: {}", uid);
        additionalAuthInfoRepository.delete(uid);
    }
}
// стало
public class AdditionalAuthInfoService {
public void save(String uid) {
final AdditionalAuthInfo additionalAuthInfo = new AdditionalAuthInfo()
.setUserId(uid);
additionalAuthInfoRepository.save(additionalAuthInfo);
}

    public void delete(String uid) {
        log.debug("delete additional auth info entry for user uid: {}", uid);
        additionalAuthInfoRepository.delete(uid);
    }
}
// комментарий
- имя класса содержало неполное имя объекта
- из имен методов исключено имя объекта класса

// было
public class DigestServiceImpl implements DigestService {
public String generateDigest(DigestDto digestDto, Long salt) {
}
}
// стало
public class DigestServiceImpl implements DigestService {
public String generate(DigestDto digestDto, Long salt) {
}
}
// комментарий
- из имени метода исключен объект класса

// было
public class LoginAttemptServiceImpl implements LoginAttemptService {
public void handleSuccessfulAttemptByCrt(String base64Certificate, CertificateParams certificateParams) {
}
public void handleFailedAttemptByCrt(String base64Certificate, Exception ex) {
}
}
// стало
public class LoginAttemptHandlerImpl implements LoginAttemptHandler {
public void handleSuccessfulByCrt(String base64Certificate, CertificateParams certificateParams) {
}
public void handleFailedByCrt(String base64Certificate, Exception ex) {
}
}
// комментарий
- имя класса не отражало предназначение класса
- из имени методов убран объект класса

// было
public class UserServiceImpl implements UserService {
public User getUser(String uid) {
return repository.getUser(uid);
}
}
// стало
public class UserServiceImpl implements UserService {
public User getByUid(String uid) {
return repository.getUser(uid);
}
}
// комментарий
- имя объекта класса исключено из названия метода

// было
public class UserServiceImpl implements UserService {
public User getByUid(String uid) {
return repository.getUser(uid);
}
public User authenticateUserByCertificateAndPassword(String certificateBase64, String password) {
}
public User authenticateUserByEmailAndPassword(String email, String password) {
}
public User authenticateUserByLoginAndPassword(String login, String password) {
}
}
// стало
public class UserServiceImpl implements UserService {
public User getByUid(String uid) {
return repository.getUser(uid);
}
}
public class UserAuthenticatorImpl implements UserAuthenticator {
public User authenticateByCertificateAndPassword(String certificateBase64, String password) {
}
public User authenticateByEmailAndPassword(String email, String password) {
}
public User authenticateByLoginAndPassword(String login, String password) {
}
}
// комментарий
- функционал аутентификации вынесен в отдельный класс
- из названий методов удалено имя объекта класса

// было
public class TokenVerifierServiceImpl implements TokenVerifierService {
public void doVerifyEncodedToken(@Nullable final String encodedToken) {
}
}
// стало
public class TokenVerifierServiceImpl implements TokenVerifierService {
public void verify(@Nullable final String encodedToken) {
}
}
// комментарий
- из имени метода убран объект класса
- название глагола метода скорректировано

// было
class TokenDecoder {
TokenDecodeInfo decodeToken(String encodedToken) {
}
}
// стало
class TokenDecoder {
TokenDecodeInfo decodeAndGetInfo(String encodedToken) {
}
}
// комментарий
- убран объект класса из имени метода
- в имя метода добавлена информация о всех выполняемых действиях

// было
public class OrganizationEnhancer implements TokenEnhancer {
public OAuth2AccessToken enhanceOrganization(OAuth2AccessToken accessToken, OAuth2Authentication authentication) {
}
}
// стало
public class OrganizationEnhancer implements TokenEnhancer {
public OAuth2AccessToken enhance(OAuth2AccessToken accessToken, OAuth2Authentication authentication) {
}
}
// комментарий
- убран объект класса из имени метода

// было
protected Authentication authentication(User user) {
return new AnonymousAuthenticationToken(
user.getUserInfo().getId(), user.getUserInfo().getId(),
AuthorityUtils.createAuthorityList(Constants.ROLE_GUEST));
}
// стало
protected Authentication getUserAuthenticated(User user) {
return new AnonymousAuthenticationToken(
user.getUserInfo().getId(), user.getUserInfo().getId(),
AuthorityUtils.createAuthorityList(Constants.ROLE_GUEST));
}
// комментарий
- использован глагол вместо существительного
- отображена информация о реальных действиях