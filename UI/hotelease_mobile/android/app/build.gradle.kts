plugins {
    id 'com.android.application'
    id 'kotlin-android'
    id 'dev.flutter.flutter-gradle-plugin'
}

android {
    namespace = "com.example.hotelease_mobile"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.example.hotelease_mobile"
        minSdk = 21
        targetSdk = 34
        versionCode 1
        versionName "1.0.0"
    }

    compileOptions {
        sourceCompatibility JavaVersion.VERSION_11
        targetCompatibility JavaVersion.VERSION_11
    }

    kotlinOptions {
        jvmTarget = "11"
    }
}

flutter {
    source = "../.."
}
