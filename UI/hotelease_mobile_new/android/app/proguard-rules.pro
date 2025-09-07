# ==============================
# Flutter Deferred Components (Google Play Core)
# ==============================
-keep class com.google.android.play.core.** { *; }
-keep class com.google.android.play.** { *; }
-keep class com.google.android.gms.** { *; }

# Ovo je veoma važno za Flutter deferred components
-keep class io.flutter.embedding.engine.deferredcomponents.** { *; }
-keep class io.flutter.embedding.android.FlutterPlayStoreSplitApplication { *; }

# ==============================
# Stripe SDK (Push Provisioning + 3DS2 + Paymentsheet) - PROŠIRENA PRAVILA
# ==============================
-keep class com.stripe.android.** { *; }
-keep class com.stripe.android.pushProvisioning.** { *; }
-keep class com.stripe.android.stripe3ds2.** { *; }
-keep class com.stripe.android.paymentsheet.** { *; }
-keep class com.stripe.android.model.** { *; }
-keep class com.stripe.android.core.** { *; }
-keep class com.reactnativestripesdk.** { *; } # Ovo je važno!

# Eksplicitna pravila za klase koje R8 ne može da nađe
-keep class com.stripe.android.pushProvisioning.PushProvisioningActivity$g { *; }
-keep class com.stripe.android.pushProvisioning.PushProvisioningActivityStarter$Args { *; }
-keep class com.stripe.android.pushProvisioning.PushProvisioningActivityStarter$Error { *; }
-keep class com.stripe.android.pushProvisioning.PushProvisioningActivityStarter { *; }
-keep class com.stripe.android.pushProvisioning.PushProvisioningEphemeralKeyProvider { *; }

# ==============================
# Opšta Flutter pravila
# ==============================
-keep class io.flutter.app.** { *; }
-keep class io.flutter.embedding.** { *; }
-keep class io.flutter.plugin.** { *; }

# Čuvanje platform-specific Flutter klase
-keep class androidx.lifecycle.DefaultLifecycleObserver