require "#{File.dirname(__FILE__)}/../test_helper"

class JournalsTest < ActionController::IntegrationTest
  fixtures :journals

  def setup
    @journals = [:atmosfera, :csalud, :octopus]
  end

   def test_getting_index
     get "/journals"
     assert_equal 200, status
     assert_equal '/journals', path
   end

   def test_showing
     @journals.each { | journal |
       post "/journals/show", :id => journals(journal).id
       assert 200, status
       assert_equal '/journals/show', path
     }
   end

   def test_editing
     @journals.each { | journal |
       post "/journals/edit", :id => journals(journal).id
       assert 200, status
       assert "/journals/edit/#{journals(journal).id}", path
     }
   end

   def test_updating_name
     @journals.each { | journal |
       post "/journals/update", :id => journals(journal).id, :title => journals(journal).title.reverse, :state => journals(journal).state.reverse, :city => journals(journal).city.reverse, :url => journals(journal).url.reverse
       assert 302, status
       follow_redirect!
       assert "/journals/show/#{journals(journal).id}", path
     }
   end

    def test_deleting
     @journals.each { | journal |
       get "/journals/destroy", :id => journals(journal).id
       assert 302, status
       assert '/journals/list',  path
     }
   end
end
