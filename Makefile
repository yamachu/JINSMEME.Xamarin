init:
	mkdir -p output

build: build-android build-ios build-forms

build-android: init clean-android
	msbuild /p:Configuration=Release /p:Platform=AnyCPU JINSMEME.Native.Android/JINSMEME.Native.Android.csproj

build-ios: init clean-ios
	msbuild /p:Configuration=Release /p:Platform=AnyCPU JINSMEME.Native.iOS/JINSMEME.Native.iOS.csproj
	msbuild /t:Pack /p:Configuration=Release /p:Platform=AnyCPU JINSMEME.Native.iOS/JINSMEME.Native.iOS.csproj

build-forms: init clean-forms
	dotnet restore JINSMEME.Forms/JINSMEME.Forms.csproj
	msbuild /t:Pack /p:Configuration=Release JINSMEME.Forms/JINSMEME.Forms.csproj

pack: pack-android pack-ios pack-forms

pack-android: build-android
	msbuild /t:Pack /p:Configuration=Release /p:Platform=AnyCPU JINSMEME.Native.Android/JINSMEME.Native.Android.csproj
	cp JINSMEME.Native.Android/bin/Release/*.nupkg output

pack-ios: build-ios
	msbuild /t:Pack /p:Configuration=Release /p:Platform=AnyCPU JINSMEME.Native.iOS/JINSMEME.Native.iOS.csproj
	cp JINSMEME.Native.iOS/bin/Release/*.nupkg output

pack-forms: build-forms
	msbuild /t:Pack /p:Configuration=Release JINSMEME.Forms/JINSMEME.Forms.csproj
	cp JINSMEME.Forms/bin/Release/*.nupkg output

clean: clean-android clean-ios clean-forms

clean-android:
	rm -rf JINSMEME.Native.Android/{bin,obj}

clean-ios:
	rm -rf JINSMEME.Native.iOS/{bin,obj}

clean-forms:
	rm -rf JINSMEME.Forms/{bin,obj}

clean-all:
	find {JINSMEME.*,SampleApp.*} -type d -maxdepth 1 \( -name bin -o -name obj \) -print|xargs -n1 rm -r
	rm -rf output

.PHONY: clean-android clean-ios clean-forms clean-all init clean pack init
