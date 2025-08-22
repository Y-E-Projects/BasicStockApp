# BasicStockApp - Kurulum ve Dağıtım Rehberi

---

## Docker ile Kurulum Önerisi

Modern, hızlı ve sorunsuz bir kurulum için BasicStockApp uygulamasını Docker üzerinden çalıştırmanız şiddetle önerilir. Docker, manuel IIS kurulumu, MySQL yüklemesi ve yerel yapılandırma gereksinimini ortadan kaldırır. Docker sürümü hem HTTP hem de HTTPS (SSL) desteği sunar ve önceden yapılandırılmış `docker-compose.yml` ve `docker-compose-ssl.yml` dosyalarını içerir.

Tam Docker kurulum rehberi ve yapılandırma dosyalarına aşağıdaki bağlantıdan ulaşabilirsiniz:
[BasicStockApp Docker Repository](https://github.com/Y-E-Projects/BasicStockApp-Api-Docker-MySQL)

---

## 1. Paket İndirme ve Çıkartma (Manuel IIS Kurulumu)

* `API.zip` dosyasını [BasicStockApp Releases](https://github.com/Y-E-Projects/BasicStockApp/releases) sayfasından indirin.
* Arşivi, tercih ettiğiniz bir dizine çıkarın (ör. `C:\Publish`).

---

## 2. IIS (Internet Information Services) Kurulumu ve Yapılandırması

* Başlat menüsünden **“Windows özelliklerini aç veya kapat”** seçeneğini açın.
* **Internet Information Services (IIS)** bileşenlerini etkinleştirin.
* IIS’in doğru şekilde kurulduğundan ve yapılandırıldığından emin olun.

---

## 3. MySQL Sunucusu Kurulumu ve Yapılandırması

* En güncel `mysql-installer-community-x.x.x.x.msi` dosyasını [MySQL Community Installer](https://dev.mysql.com/downloads/installer/) sayfasından indirin.
* Kurulum adımlarını bu [video eğitimi](https://www.youtube.com/watch?v=v8i2NgiM5pE&ab_channel=GeekyScript) ile takip edin.

  * Önemli: Veritabanı erişimi için özel bir kullanıcı oluşturabilir veya root hesabını kullanabilirsiniz.
* Kurulum sonrası sistemi yeniden başlatın.

---

## 4. IIS Üzerinde Web Sitesi Tanımlama

* Başlat menüsünden **Internet Information Services (IIS) Manager**’ı başlatın.
* Sol paneldeki **Sites** üzerinde sağ tıklayarak **Add Website** seçeneğini seçin.
* Açılan pencerede:

  * Sadece İngilizce karakterler kullanarak ve boşluk bırakmadan bir **Site Name** girin.
  * **Physical Path** alanına, çıkarttığınız dizini girin (ör. `C:\Publish`).
* Klasör izinlerini kontrol edin: dizine sağ tıklayın, **Properties > Security** yolunu izleyin ve **IIS\_IUSRS** kullanıcısının gerekli izinlere sahip olduğundan emin olun; gerekirse ekleyin.
* Binding türünü **https** olarak ayarlayın.
* IP adresi alanını boş bırakın ve kurulumu tamamlayın.

---

## 5. Uygulama Yapılandırması (`appsettings.json`)

* Yayın dizininde bulunan `appsettings.json` dosyasını bir metin düzenleyici ile açın.
* `DefaultConnection` altındaki `Database` alanını, boşluk ve özel karakter içermeyen bir veritabanı adı ile güncelleyin.
* MySQL kullanıcı adı ve şifresini `User` ve `Password` alanlarına girin:

  * Özel bir kullanıcı oluşturmadıysanız `"root"` ve root şifrenizi kullanın.
* `DefaultCulture` parametresini tercih ettiğiniz dil için ayarlayın:

  * Türkçe: `"tr-TR"`
  * İngilizce: `"en-US"`
* Dosyayı kaydedin.

---

## 6. Uygulama Başlatma ve İlk Veritabanı Migrasyonu

* Yayın dizininde `API.exe` dosyasını çalıştırın.
* Konsolda Entity Framework Core migration loglarını izleyin, örnek:

```
Executed DbCommand (6ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
CREATE DATABASE DenemeStockAppDb
...
Applying migration '20250812141150_mig1'
...
Application started. Press Ctrl+C to shut down.
```

* Bu loglar veritabanının başarıyla oluşturulduğunu ve şema uygulamasının tamamlandığını doğrular.

---

## 7. Son Adımlar ve Test

* Kurulum sonrası tarayıcınızdan `https://localhost` adresine giderek uygulamanın çalıştığını doğrulayın.
* Yanıt dili veya veritabanı ayarlarında değişiklik yapmak için:

  * `API.exe` uygulamasını yeniden başlatın.
  * Uygulamayı barındıran IIS sitesini yeniden başlatın.
  * Dil değişiklikleri için IIS’i 2-3 kez yeniden başlatmak gerekebilir.

---

### Önemli Notlar:

* Veritabanı bağlantı dizeleri ve uygulama dili değişiklikleri için hem IIS’in hem de uygulamanın yeniden başlatılması gereklidir.
* IIS kullanıcı izinlerinin doğru yapılandırılması uygulamanın gerekli dosyalara erişimi için kritiktir.
* Güvenli iletişim için geçerli bir SSL sertifikası ile HTTPS yapılandırmanız şiddetle önerilir.

---

Bu rehber, hem Docker tabanlı hızlı kurulum önerisini hem de manuel IIS tabanlı kurulum adımlarını içermektedir. En verimli ve profesyonel kurulum için Docker kullanımı önerilir.
