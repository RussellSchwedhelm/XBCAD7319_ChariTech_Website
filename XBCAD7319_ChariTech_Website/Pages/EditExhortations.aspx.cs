using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XBCAD7319_ChariTech_Website.Pages
{
    public partial class Exhortations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}

/*
 
    This is the Javascript code to get 'hypothetically' this pge's play audio component working    

< script >
    let isPlaying = false;

function togglePlayPause()
{
    const playButton = document.querySelector('.play-button');
    const audio = document.querySelector('audio');

    if (isPlaying)
    {
        audio.pause();
        playButton.textContent = '▶';
    }
    else
    {
        audio.play();
        playButton.textContent = '❚❚';
    }

    isPlaying = !isPlaying;
}

document.addEventListener('DOMContentLoaded', function() {
    const progressBar = document.querySelector('.progress');
    const currentTimeElement = document.querySelector('.current-time');
    const totalTimeElement = document.querySelector('.total-time');

    // Example values, replace with actual audio data
    const totalDuration = 1239; // Total duration in seconds (20:39)
    const currentTime = 611; // Current time in seconds (10:11)

    // Update progress bar width
    const progressPercentage = (currentTime / totalDuration) * 100;
    progressBar.style.width = progressPercentage + '%';

    // Update timestamps
    currentTimeElement.textContent = formatTime(currentTime);
    totalTimeElement.textContent = formatTime(totalDuration);

    function formatTime(seconds)
    {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;
        return `${ minutes}:${ remainingSeconds.toString().padStart(2, '0')}`;
}
    });
</ script >

*/
