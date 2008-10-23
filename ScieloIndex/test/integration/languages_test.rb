require "#{File.dirname(__FILE__)}/../test_helper"

class LanguagesTest < ActionController::IntegrationTest
  fixtures :languages, :publishers, :collections, :institutions

  def setup
    @languages = [:klingon, :quenya, :sindarin]
    @mylanguage = {:name => 'Kriptonese', :code => 'kp'}
  end

   def test_getting_index
     get "/languages"
     assert_equal 200, status
     assert_equal '/languages', path
   end

   def test_showing
     @languages.each { | language |
       post "/languages/show", :id => languages(language).id
       assert 200, status
       assert_equal '/languages/show', path
     }
   end

   def test_editing
     @languages.each { | language |
       post "/languages/edit", :id => languages(language).id
       assert 200, status
       assert "/languages/edit/#{languages(language).id}", path
     }
   end

   def test_updating_name
     @languages.each { | language |
       post "/languages/update", :id => languages(language).id, :name => languages(language).name.reverse
       assert 302, status
       follow_redirect!
       assert "/languages/show/#{languages(language).id}", path
     }
   end

    def test_deleting
     @languages.each { | language |
       get "/languages/destroy", :id => languages(language).id
       assert 302, status
       assert '/languages/list',  path
     }
   end
end
