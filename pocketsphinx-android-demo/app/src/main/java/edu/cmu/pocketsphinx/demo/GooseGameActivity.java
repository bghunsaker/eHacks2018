/*Developed by Bryson Hunsaker, Jacob Bacon, Nick Kirkpatrick, and Dipak
Subramaniam at eHacks 2018, St. Louis Mkissouri*/
/* ====================================================================
 * Copyright (c) 2014 Alpha Cephei Inc.  All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in
 *    the documentation and/or other materials provided with the
 *    distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY ALPHA CEPHEI INC. ``AS IS'' AND
 * ANY EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL CARNEGIE MELLON UNIVERSITY
 * NOR ITS EMPLOYEES BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * ====================================================================
 */

package edu.cmu.pocketsphinx.demo;
import android.Manifest;
import android.app.Activity;
import android.app.Instrumentation;
import android.content.pm.PackageManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.view.KeyEvent;
import android.widget.TextView;
import android.widget.Toast;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.lang.ref.WeakReference;
import java.util.HashMap;

import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import edu.cmu.pocketsphinx.SpeechRecognizer;
import edu.cmu.pocketsphinx.SpeechRecognizerSetup;

import static android.widget.Toast.makeText;

public class GooseGameActivity extends Activity implements
        RecognitionListener {

    private static final String COMMAND_SEARCH = "commands";
    private static final int PERMISSIONS_REQUEST_RECORD_AUDIO = 1;
    private static String currCommandToShare = "";


    private SpeechRecognizer recognizer;
    private HashMap<String, Integer> captions;

    @Override
    public void onCreate(Bundle state) {
        super.onCreate(state);

        // Prepare the data for UI
        captions = new HashMap<>();
        captions.put(COMMAND_SEARCH, R.string.command_caption);
        setContentView(R.layout.main);
        ((TextView) findViewById(R.id.caption_text))
                .setText("Preparing the recognizer");

        // Check if user has given permission to record audio
        int permissionCheck = ContextCompat.checkSelfPermission(getApplicationContext(), Manifest.permission.RECORD_AUDIO);
        if (permissionCheck != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.RECORD_AUDIO}, PERMISSIONS_REQUEST_RECORD_AUDIO);
            return;
        }
        // Recognizer initialization is a time-consuming and it involves IO,
        // so we execute it in async task
        new SetupTask(this).execute();
    }

    private static class SetupTask extends AsyncTask<Void, Void, Exception> {
        WeakReference<GooseGameActivity> activityReference;
        SetupTask(GooseGameActivity activity) {
            this.activityReference = new WeakReference<>(activity);
        }
        @Override
        protected Exception doInBackground(Void... params) {
            try {
                Assets assets = new Assets(activityReference.get());
                File assetDir = assets.syncAssets();
                activityReference.get().setupRecognizer(assetDir);
            } catch (IOException e) {
                return e;
            }
            return null;
        }
        @Override
        protected void onPostExecute(Exception result) {
            if (result != null) {
                ((TextView) activityReference.get().findViewById(R.id.caption_text))
                        .setText("Failed to init recognizer " + result);
            } else {
                activityReference.get().switchSearch(COMMAND_SEARCH);
            }
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           @NonNull String[] permissions, @NonNull  int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);

        if (requestCode == PERMISSIONS_REQUEST_RECORD_AUDIO) {
            if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                // Recognizer initialization is a time-consuming and it involves IO,
                // so we execute it in async task
                new SetupTask(this).execute();
            } else {
                finish();
            }
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();

        if (recognizer != null) {
            recognizer.cancel();
            recognizer.shutdown();
        }
    }

    /**
     * In partial result we get quick updates about current hypothesis. In
     * keyword spotting mode we can react here, in other modes we need to wait
     * for final result in onResult.
     */
    @Override
    public void onPartialResult(Hypothesis hypothesis) {
        System.out.println("On partial result");
        if (hypothesis == null)
            return;

        String text = hypothesis.getHypstr();
        if (text.equals(COMMAND_SEARCH))
            switchSearch(COMMAND_SEARCH);
        else {
            ((TextView) findViewById(R.id.result_text)).setText(text);
            //Simulates keystrokes corresponding to the word that popped up
            //simulateKeyPresses(text);
            //This command will be passed via a shared intent.actionview
            currCommandToShare = text;
        }


    }

    /**
     * This callback is called when we stop the recognizer.
     */
    @Override
    public void onResult(Hypothesis hypothesis) {
        ((TextView) findViewById(R.id.result_text)).setText("");
        if (hypothesis != null) {
            String text = hypothesis.getHypstr();
            makeText(getApplicationContext(), text, Toast.LENGTH_SHORT).show();
try {

    //simulateKeyPresses(text);
    FileOutputStream fOut = openFileOutput("commands.text",  MODE_WORLD_READABLE);
    fOut.close();

    } catch(IOException e) { }

            //This command will be passed via a shared intent.actionview
            //currCommandToShare = text;
        }
    }

    @Override
    public void onBeginningOfSpeech() {
    }

    /**
     * We stop recognizer here to get a final result
     */
    @Override
    public void onEndOfSpeech() {
    }

//    public void simulateKeyPresses(String text){
//        System.out.println("\n\n\n***************IN CASE********************\n\n\n");
//        //Press a key corresponding to the word being output
//        //Instrumentation inst = new Instrumentation();
////        switch(text) {
//
////            case "jump":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_W));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_W));
////                //inst.sendKeyDownUpSync(KeyEvent.KEYCODE_W);
////                break;
////            case "duck":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_S));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_S));
////                break;
////            case "shoot":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_P));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_P));
////                break;
////            case "back":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_A));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_A));
////                break;
////            case "forward":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_D));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_D));
////                break;
////            case "start":
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_DOWN, KeyEvent.KEYCODE_Q));
////                dispatchKeyEvent(new KeyEvent(KeyEvent.ACTION_UP, KeyEvent.KEYCODE_Q));
////                break;
////        }
//
//
//
//
//    }


    private void switchSearch(String searchName) {
        recognizer.stop();

        recognizer.startListening(searchName, 10000);

        String caption = getResources().getString(captions.get(searchName));
        ((TextView) findViewById(R.id.caption_text)).setText(caption);
    }

    private void setupRecognizer(File assetsDir) throws IOException {
        // The recognizer can be configured to perform multiple searches
        // of different kind and switch between them

        recognizer = SpeechRecognizerSetup.defaultSetup()
                .setAcousticModel(new File(assetsDir, "en-us-ptm"))
                .setDictionary(new File(assetsDir, "cmudict-en-us.dict"))

                .setRawLogDir(assetsDir) // To disable logging of raw audio comment out this call (takes a lot of space on the device)

                .getRecognizer();
        recognizer.addListener(this);

        /* In your application you might not need to add all those searches.
          They are added here for demonstration. You can leave just one.
         */


        // Create grammar-based search for game command recognition
        File cmdGrammar = new File(assetsDir, "commands.gram");
        recognizer.addGrammarSearch(COMMAND_SEARCH, cmdGrammar);
    }

    @Override
    public void onError(Exception error) {
        ((TextView) findViewById(R.id.caption_text)).setText(error.getMessage());
    }

    @Override
    public void onTimeout() {
    }

    public static String shareCommand() {
        return currCommandToShare;
    }

}