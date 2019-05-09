package info.sweetduet.ksnctf.jewel;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;
import java.io.InputStream;
import java.math.BigInteger;
import java.security.Key;
import java.security.MessageDigest;
import java.security.spec.AlgorithmParameterSpec;
import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

public class JewelActivity extends Activity {
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(R.layout.main);
        String deviceId = ((TelephonyManager) getSystemService("phone")).getDeviceId();
        try {
            MessageDigest instance = MessageDigest.getInstance("SHA-256");
            instance.update(deviceId.getBytes("ASCII"));
            String bigInteger = new BigInteger(instance.digest()).toString(16);
            if (!deviceId.substring(0, 8).equals("99999991")) {
                new Builder(this).setMessage("Your device is not supported").setCancelable(false).setPositiveButton("OK", new C0001b(this)).show();
            } else if (bigInteger.equals("356280a58d3c437a45268a0b226d8cccad7b5dd28f5d1b37abf1873cc426a8a5")) {
                InputStream openRawResource = getResources().openRawResource(R.raw.jewel_c);
                byte[] bArr = new byte[openRawResource.available()];
                openRawResource.read(bArr);
                Key secretKeySpec = new SecretKeySpec(("!" + deviceId).getBytes("ASCII"), "AES");
                AlgorithmParameterSpec ivParameterSpec = new IvParameterSpec("kLwC29iMc4nRMuE5".getBytes());
                Cipher instance2 = Cipher.getInstance("AES/CBC/PKCS5Padding");
                instance2.init(2, secretKeySpec, ivParameterSpec);
                byte[] doFinal = instance2.doFinal(bArr);
                View imageView = new ImageView(this);
                imageView.setImageBitmap(BitmapFactory.decodeByteArray(doFinal, 0, doFinal.length));
                setContentView(imageView);
            } else {
                new Builder(this).setMessage("You are not a valid user").setCancelable(false).setPositiveButton("OK", new C0000a(this)).show();
            }
        } catch (Exception e) {
            Toast.makeText(this, e.toString(), 1).show();
        }
    }
}
