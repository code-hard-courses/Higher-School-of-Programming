пример1
=======
public class UserInfo implements DataToHexString, Serializable {
@JsonProperty("uid")
private String id;
@JsonProperty("cn")
private String name;
@JsonProperty("orgId")
private String[] organizations;
private String login;
}
// organizations может быть заменен на List<String>

пример2
=======
public class Inventory extends Doc {
@Schema(description = "Время последнего обновления данных мониторинга",
example = "1595687904")
@JsonProperty("updateTimestamp")
private Long updateTimestamp;

    @Schema(description = "Состояние коннекта к мастеру",
            example = "true")
    @JsonProperty("isConnected")
    private Boolean connected;

    @Schema(description = "Тип устройства",
            example = "ARM")
    @JsonProperty("devType")
    private DevType devType;

    @Schema(description = "IP шлюз устройства",
            example = "10.26.4.1")
    @JsonProperty("ip4_gw")
    private String ipGateway;
    @Schema(description = "Массив с IP адресами устройства",
            example = " [\n\"10.26.6.107\"\n]")
    @JsonProperty("fqdn_ip4")
    private String[] ipAddresses;
}
// ipAddresses может быть заменен на List<String>

пример3
=======
@JsonIgnore
private MetricValue getOrCompute(String[] labels) {
final Optional<MetricValue> metricValueOptional = metricValues
.stream()
.filter(metricValue -> Arrays.equals(labels, metricValue.getLables().toArray()))
.findAny();
final MetricValue metricValue;
if (metricValueOptional.isPresent()) {
metricValue = metricValueOptional.get();
} else {
metricValue = new MetricValue(labels);
metricValues.add(metricValue);
}
return metricValue;
}
// labels может быть заменен на List<String>

пример4
=======
public static final String[] VIEW_HASH_ARRAY = {"1e6e9d717d3b11a56ce32ee02c77358592a1dbfe", "365b723cba50142a1dbd6913a518517b4ff000fa", "64b3870ee98918527cd8294c9152c940446b7b11"};
// может быть заменен на List<String>

пример5
=======
private static class DataLocation {
private Infomat[] data;
}
// data может быть заменен на List<Infomat>
